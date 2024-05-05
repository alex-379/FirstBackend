using AutoMapper;
using FirstBackend.Buiseness.Configuration;
using FirstBackend.Buiseness.Interfaces;
using FirstBackend.Buiseness.Models.Tokens.Requests;
using FirstBackend.Buiseness.Models.Users.Requests;
using FirstBackend.Buiseness.Models.Users.Responses;
using FirstBackend.Core.Dtos;
using FirstBackend.Core.Enums;
using FirstBackend.Core.Exeptions;
using FirstBackend.DataLayer.Contexts;
using FirstBackend.DataLayer.Interfaces;
using FluentValidation;
using FluentValidation.Results;
using Serilog;
using System.Security.Claims;
using System.Transactions;

namespace FirstBackend.Buiseness.Services;

public class UsersService(IUsersRepository usersRepository, ISaltsRepository saltsRepository, IPasswordsService passwordsService, ITokensService tokensService, IMapper mapper, JwtToken jwt,
    SaltLxContext contextSalt, MainerLxContext contextMainer)
    : IUsersService
{
    private readonly IUsersRepository _usersRepository = usersRepository;
    private readonly ISaltsRepository _saltsRepository = saltsRepository;
    private readonly IPasswordsService _passwordsService = passwordsService;
    private readonly ITokensService _tokensService = tokensService;
    private readonly IMapper _mapper = mapper;
    private readonly JwtToken _jwt = jwt;
    private readonly SaltLxContext _ctxSalt = contextSalt;
    private readonly MainerLxContext _ctxMainer = contextMainer;
    private readonly ILogger _logger = Log.ForContext<UsersService>();

    public Guid AddUser(CreateUserRequest request)
    {
        var user = _mapper.Map<UserDto>(request);
        _logger.Information("Устанавливаем роль Клиент");
        user.Role = UserRole.Client;
        _logger.Information("Переводим почту и имя в нижний регистр");
        user.Mail = user.Mail.ToLower();
        user.Name = user.Name.ToLower();
        _logger.Information("Проверяем был ли зарегестрирован пользователь с такой почтой ранее");
        if (_usersRepository.GetUserByMail(user.Mail) is not null)
        {
            throw new ValidationDataException("Такой e-mail уже существует");
        }

        user.Password = _passwordsService.HashPasword(user.Password, out var salt);
        //var validationResult = _validator.Validate(user);
        //if (!validationResult.IsValid)
        //{
        //    throw new ValidationException(validationResult.Errors);
        //}
        using var transactionMainerContext = _ctxMainer.Database.BeginTransaction();
        using var transactionSaltContext = _ctxSalt.Database.BeginTransaction();
        try
        {
            _logger.Information("Обращаемся к методу репозитория Создание нового пользователя");
            _usersRepository.AddUser(user);
            _logger.Information($"Создан новый пользователь с ID {user.Id}");

            SaltDto saltDto = new()
            {
                Salt = Convert.ToHexString(salt),
                UserId = user.Id
            };

            _logger.Information("Обращаемся к методу репозитория Добавление соли для пользователя");
            _saltsRepository.AddSalt(saltDto);

            transactionMainerContext.Commit();
            transactionSaltContext.Commit();
        }
        catch (TransactionException ex)
        {
            transactionMainerContext.Rollback();
            transactionSaltContext.Rollback();
            Log.Error(ex.Message);
        }

        return user.Id;
    }

    public AuthenticatedResponse LoginUser(LoginUserRequest request)
    {
        _logger.Information("Проверяем переданы ли данные");
        if (request is null)
        {
            throw new BadRequestException("Передайте входные данные");
        }
        UserDto user = _mapper.Map<UserDto>(request);

        _logger.Information("Проверяем есть ли такой пользователь в базе данных");
        var userDb = _usersRepository.GetUserByMail(user.Mail.ToLower()) ?? throw new UnauthorizedException("Не пройдена аутентификация");

        _logger.Information("Проверка аутентификационных данных");
        var salt = Convert.FromHexString(_saltsRepository.GetSaltByUserId(userDb.Id).Salt);
        var confirmPassword = _passwordsService.VerifyPassword(user.Password, userDb.Password, salt);
        if (!confirmPassword)
        {
            throw new UnauthorizedException("Не пройдена аутентификация");
        }

        var claims = new List<Claim>
        {
        new(ClaimTypes.Name, userDb.Name),
        new(ClaimTypes.Email, userDb.Mail),
        new(ClaimTypes.Role, userDb.Role.ToString()),
        };

        var accessToken = _tokensService.GenerateAccessToken(claims);
        var refreshToken = _tokensService.GenerateRefreshToken();
        userDb.RefreshToken = refreshToken;
        userDb.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(_jwt.LifeTimeRefreshToken);
        _usersRepository.UpdateUser(userDb);

        return new AuthenticatedResponse
        {
            Token = accessToken,
            RefreshToken = refreshToken
        };
    }

    public List<UserResponse> GetAllUsers()
    {
        _logger.Information("Обращаемся к методу репозитория Получение всех пользователей");
        var users = _mapper.Map<List<UserResponse>>(_usersRepository.GetAllUsers());

        return users;
    }

    public UserFullResponse GetUserById(Guid id)
    {
        _logger.Information($"Обращаемся к методу репозитория Получение пользователя по ID {id}");
        var user = _usersRepository.GetUserById(id) ?? throw new NotFoundException($"Пользователь с ID {id} не найден");
        var userResponse = _mapper.Map<UserFullResponse>(user);

        return userResponse;
    }

    public void UpdateUser(Guid userId, UpdateUserDataRequest request)
    {
        _logger.Information($"Проверяем существует ли пользователь с ID {userId}");
        var user = _usersRepository.GetUserById(userId) ?? throw new NotFoundException($"Пользователь с ID {userId} не найден");
        _logger.Information($"Обновляем данные пользователя с ID {userId} из запроса");
        user.Name = request.Name.ToLower();
        _logger.Information($"Обращаемся к методу репозитория Обновление пользователя с ID {user.Id}");
        _usersRepository.UpdateUser(user);
    }

    public AuthenticatedResponse UpdateUserPassword(Guid userId, UpdateUserPasswordRequest request, string accessToken)
    {
        _logger.Information($"Проверяем существует ли пользователь с ID {userId}");
        var user = _usersRepository.GetUserById(userId) ?? throw new NotFoundException($"Пользователь с ID {userId} не найден");
        _logger.Information("Проверяем соответствует ли пароль требуемой длине");
        if (string.IsNullOrEmpty(request.Password) || request.Password.Length < 8)
        {
            throw new ValidationDataException("Пароль должен быть не менее 8 символов");
        }

        _logger.Information($"Обновляем пароль пользователя с ID {userId} из запроса");
        user.Password = request.Password;
        user.Password = _passwordsService.HashPasword(user.Password, out var salt);
        using var transactionMainerContext = _ctxMainer.Database.BeginTransaction();
        using var transactionSaltContext = _ctxSalt.Database.BeginTransaction();
        try
        {
            _logger.Information($"Обращаемся к методу репозитория Обновление пользователя с ID {user.Id}");
            _usersRepository.UpdateUser(user);

            _logger.Information($"Обращаемся к методу репозитория Получение соли пользователя с ID {user.Id}");
            var saltDb = _saltsRepository.GetSaltByUserId(user.Id);
            saltDb.Salt = Convert.ToHexString(salt);
            _logger.Information("Обращаемся к методу репозитория Обновление соли для пользователя");
            _saltsRepository.UpdateSalt(saltDb);
            _logger.Information($"Обновлена соль для пользователя с ID {user.Id}");

            transactionMainerContext.Commit();
            transactionSaltContext.Commit();
        }
        catch (TransactionException ex)
        {
            transactionMainerContext.Rollback();
            transactionSaltContext.Rollback();
            Log.Error(ex.Message);
        }

        _logger.Information($"Обновляем токен пользователя с ID {userId}");
        var authenticatedResponse = _tokensService.Refresh(new RefreshTokenRequest
        {
            AccessToken = accessToken,
            RefreshToken = user.RefreshToken
        });

        return authenticatedResponse;
    }

    public void UpdateUserMail(Guid userId, UpdateUserMailRequest request)
    {
        _logger.Information($"Проверяем существует ли пользователь с ID {userId}");
        var user = _usersRepository.GetUserById(userId) ?? throw new NotFoundException($"Пользователь с ID {userId} не найден");
        _logger.Information($"Проверяем есть ли пользователь c почтой из запроса в базе данных");
        if ((_usersRepository.GetUserByMail(request.Mail.ToLower()) is not null))
        {
            throw new ValidationDataException("Такой e-mail уже существует");
        }

        _logger.Information($"Обновляем почту пользователя с ID {userId}");
        user.Mail = request.Mail.ToLower();
        _logger.Information($"Обращаемся к методу репозитория Обновление пользователя с ID {user.Id}");
        _usersRepository.UpdateUser(user);
    }

    public void DeleteUserById(Guid id)
    {
        _logger.Information($"Проверяем существует ли пользователь с ID {id}");
        var user = _usersRepository.GetUserById(id) ?? throw new NotFoundException($"Пользователь с ID {id} не найден");
        using var transactionMainerContext = _ctxMainer.Database.BeginTransaction();
        using var transactionSaltContext = _ctxSalt.Database.BeginTransaction();
        try
        {
            _logger.Information($"Обращаемся к методу репозитория Удаление пользователя по ID {id}");
            _usersRepository.DeleteUser(user);

            _logger.Information($"Обращаемся к методу репозитория Получение соли пользователя с ID {user.Id}");
            var salt = _saltsRepository.GetSaltByUserId(user.Id);
            _logger.Information($"Обращаемся к методу репозитория Удаление соли пользователя с ID {user.Id}");
            _saltsRepository.DeleteSalt(salt);

            transactionMainerContext.Commit();
            transactionSaltContext.Commit();
        }
        catch (TransactionException ex)
        {
            transactionMainerContext.Rollback();
            transactionSaltContext.Rollback();
            Log.Error(ex.Message);
        }

    }
}
