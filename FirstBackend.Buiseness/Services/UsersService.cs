using AutoMapper;
using FirstBackend.Buiseness.Interfaces;
using FirstBackend.Buiseness.Models.Users.Requests;
using FirstBackend.Core.Dtos;
using FirstBackend.Core.Enums;
using FirstBackend.Core.Exeptions;
using FirstBackend.DataLayer.Interfaces;
using Serilog;
using System.Security.Claims;

namespace FirstBackend.Buiseness.Services;

public class UsersService(IUsersRepository usersRepository, ISaltsRepository saltsRepository, IPasswordsService passwordsService, ITokensService tokensService, IMapper mapper)
    : IUsersService
{
    private readonly IUsersRepository _usersRepository = usersRepository;
    private readonly ISaltsRepository _saltsRepository = saltsRepository;
    private readonly IPasswordsService _passwordsService = passwordsService;
    private readonly ITokensService _tokensService = tokensService;
    private readonly IMapper _mapper = mapper;
    private readonly ILogger _logger = Log.ForContext<UsersService>();

    public Guid AddUser(string secret, CreateUserRequest request)
    {
        UserDto user = _mapper.Map<UserDto>(request);
        _logger.Information($"Устанавливаем роль Клиент");
        user.Role = UserRole.Client;
        _logger.Information($"Проверяем соответствует ли пароль требуемой длине");
        if (string.IsNullOrEmpty(user.Password) || user.Password.Length < 8)
        {
            throw new ValidationException("Пароль должен быть не менее 8 символов");
        }

        _logger.Information($"Проверяем был ли зарегестрирован пользователь с такой почтой ранее");
        if (_usersRepository.GetAllUsers().Any(u => u.Mail.Equals(user.Mail, StringComparison.CurrentCultureIgnoreCase)))
        {
            throw new ValidationException("Такой e-mail уже существует");
        }

        user.Password = _passwordsService.HashPasword(secret, user.Password, out var salt);
        _logger.Information($"Обращаемся к методу репозитория Создание нового пользователя");
        _usersRepository.AddUser(user);
        _logger.Information($"Создан новый пользователь с ID {user.Id}");

        SaltDto saltDto = new()
        {
            Salt = Convert.ToHexString(salt),
            UserId = user.Id
        };

        _logger.Information($"Обращаемся к методу репозитория Добавление соли для пользователя");
        _saltsRepository.AddSalt(saltDto);
        _logger.Information($"Добавлена соль для пользователя с ID {user.Id}");

        return user.Id;
    }

    public string LoginUser(string secretPassword, string secretToken, UserDto user)
    {
        _logger.Information($"Проверяем переданы ли данные");
        if (user is null)
        {
            throw new BadRequestException("Передайте входные данные");
        }

        _logger.Information($"Проверяем есть ли такой пользователь в базе данных");
        var userDb = _usersRepository.GetAllUsers().FirstOrDefault(u => u.Mail.Equals(user.Mail, StringComparison.CurrentCultureIgnoreCase))
            ?? throw new UnauthorizedException("Не пройдена аутентификация");

        _logger.Information($"Проверка аутентификационных данных");
        var salt = Convert.FromHexString(_saltsRepository.GetSaltByUserId(userDb.Id).Salt);
        var confirmPassword = _passwordsService.VerifyPassword(secretPassword, user.Password, userDb.Password, salt);
        if (!confirmPassword)
        {
            throw new UnauthorizedException("Не пройдена аутентификация");
        }

        var claims = new List<Claim>
        {
        new(ClaimTypes.Email, userDb.Mail),
        new(ClaimTypes.Role, userDb.Role.ToString()),
        };

        var tokenString = _tokensService.GenerateAccessToken(secretToken, claims);

        return tokenString;
    }

    public List<UserDto> GetAllUsers()
    {
        _logger.Information($"Обращаемся к методу репозитория Получение всех пользователей");

        return _usersRepository.GetAllUsers();
    }

    public UserDto GetUserById(Guid id)
    {
        _logger.Information($"Обращаемся к методу репозитория Получение пользователя по ID {id}");
        var user = _usersRepository.GetUserById(id) ?? throw new NotFoundException($"Пользователь с ID {id} не найден");

        return user;
    }

    public void UpdateUser(Guid userId, UserDto userUpdate)
    {
        _logger.Information($"Проверяем существует ли пользователь с ID {userId}");
        var user = _usersRepository.GetUserById(userId) ?? throw new NotFoundException($"Пользователь с ID {userId} не найден");
        _logger.Information($"Обновляем данные пользователя с ID {userId} из запроса");
        user.UserName = userUpdate.UserName;
        user.Mail = userUpdate.Mail;
        user.Password = userUpdate.Password;
        _logger.Information($"Обращаемся к методу репозитория Обновление пользователя с ID {user.Id}");
        _usersRepository.UpdateUser(user);
    }

    public void DeleteUserById(Guid id)
    {
        _logger.Information($"Проверяем существует ли пользователь с ID {id}");
        var user = _usersRepository.GetUserById(id) ?? throw new NotFoundException($"Пользователь с ID {id} не найден");
        _logger.Information($"Обращаемся к методу репозитория Удаление пользователя по ID {id}");
        _usersRepository.DeleteUser(user);
    }
}
