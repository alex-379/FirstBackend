using AutoMapper;
using FirstBackend.Business.Configuration;
using FirstBackend.Business.Interfaces;
using FirstBackend.Business.Models.Users.Requests;
using FirstBackend.Business.Models.Users.Responses;
using FirstBackend.Core.Constants.Exceptions.Business;
using FirstBackend.Core.Constants.Logs.Business;
using FirstBackend.Core.Dtos;
using FirstBackend.Core.Enums;
using FirstBackend.Core.Exсeptions;
using FirstBackend.DataLayer.Contexts;
using FirstBackend.DataLayer.Interfaces;
using Serilog;
using System.Security.Claims;

namespace FirstBackend.Business.Services;

public class UsersService(IUsersRepository usersRepository, ISaltsRepository saltsRepository, IOrdersRepository ordersRepository,
    ITransactionsRepository<MainerLxContext> transactionMainerLxRepository, ITransactionsRepository<SaltLxContext> transactionSaltLxRepository,
    IPasswordsService passwordsService, ITokensService tokensService, IMapper mapper, JwtToken jwt) : IUsersService
{
    private readonly IUsersRepository _usersRepository = usersRepository;
    private readonly ISaltsRepository _saltsRepository = saltsRepository;
    private readonly IOrdersRepository _ordersRepository = ordersRepository;
    private readonly ITransactionsRepository<MainerLxContext> _transactionMainerLxRepository = transactionMainerLxRepository;
    private readonly ITransactionsRepository<SaltLxContext> _transactionSaltLxRepository = transactionSaltLxRepository;
    private readonly IPasswordsService _passwordsService = passwordsService;
    private readonly ITokensService _tokensService = tokensService;
    private readonly IMapper _mapper = mapper;
    private readonly JwtToken _jwt = jwt;
    private readonly ILogger _logger = Log.ForContext<UsersService>();

    public Guid AddUser(CreateUserRequest request)
    {
        var user = _mapper.Map<UserDto>(request);
        if (_usersRepository.GetUserByMail(user.Mail.ToLower()) is not null)
        {
            throw new ConflictException(UsersServiceExceptions.ConflictException);
        }
        _logger.Information(UsersServiceLogs.SetRoleClient);
        user.Role = UserRole.Client;
        _logger.Information(UsersServiceLogs.SetLowerRegister);
        user.Mail = user.Mail.ToLower();
        user.Name = user.Name.ToLower();
        var (hash, salt) = _passwordsService.HashPasword(user.Password);
        user.Password = hash;
        using var transactionMainerLxContext = _transactionMainerLxRepository.BeginTransaction();
        using var transactionSaltLxContext = _transactionSaltLxRepository.BeginTransaction();
        try
        {
            _logger.Information(UsersServiceLogs.AddUser);
            user.Id = _usersRepository.AddUser(user);
            _logger.Information(UsersServiceLogs.CompleteUser, user.Id);

            SaltDto saltDto = new()
            {
                Salt = salt,
                UserId = user.Id
            };

            _logger.Information(UsersServiceLogs.AddSalt);
            _saltsRepository.AddSalt(saltDto);
            _transactionMainerLxRepository.CommitTransaction(transactionMainerLxContext);
            _transactionSaltLxRepository.CommitTransaction(transactionSaltLxContext);
        }
        catch (Exception ex)
        {
            transactionMainerLxContext.Rollback();
            transactionSaltLxContext.Rollback();
            Log.Error(ex.Message);
        }

        return user.Id;
    }

    public AuthenticatedResponse LoginUser(LoginUserRequest request)
    {
        UserDto user = _mapper.Map<UserDto>(request);

        _logger.Information(UsersServiceLogs.CheckUserByMail);
        var userDb = _usersRepository.GetUserByMail(user.Mail.ToLower())
            ?? throw new UnauthenticatedException();

        _logger.Information(UsersServiceLogs.CheckUserPassword);
        var salt = _saltsRepository.GetSaltByUserId(userDb.Id).Salt;
        var confirmPassword = _passwordsService.VerifyPassword(user.Password, userDb.Password, salt);
        if (!confirmPassword)
        {
            throw new UnauthenticatedException();
        }

        var claims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, userDb.Id.ToString()),
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
            AccessToken = accessToken,
            RefreshToken = refreshToken
        };
    }

    public List<UserResponse> GetUsers()
    {
        _logger.Information(UsersServiceLogs.GetUsers);
        var users = _mapper.Map<List<UserResponse>>(_usersRepository.GetUsers());

        return users;
    }

    public UserFullResponse GetUserById(Guid id)
    {
        _logger.Information(UsersServiceLogs.GetUserById);
        var user = _usersRepository.GetUserById(id)
            ?? throw new NotFoundException(string.Format(UsersServiceExceptions.NotFoundException, id));
        var userResponse = _mapper.Map<UserFullResponse>(user);

        return userResponse;
    }

    public void UpdateUser(Guid userId, UpdateUserDataRequest request)
    {
        _logger.Information(UsersServiceLogs.CheckUserById, userId);
        var user = _usersRepository.GetUserById(userId)
            ?? throw new NotFoundException(string.Format(UsersServiceExceptions.NotFoundException, userId));
        _logger.Information(UsersServiceLogs.UpdateUserData, userId);
        user.Name = request.Name.ToLower();
        _logger.Information(UsersServiceLogs.UpdateUserById, userId);
        _usersRepository.UpdateUser(user);
    }

    public void UpdateUserPassword(Guid userId, UpdateUserPasswordRequest request)
    {
        _logger.Information(UsersServiceLogs.CheckUserById, userId);
        var user = _usersRepository.GetUserById(userId)
            ?? throw new NotFoundException(string.Format(UsersServiceExceptions.NotFoundException, userId));
        _logger.Information(UsersServiceLogs.UpdateUserPassword, userId);
        user.Password = request.Password;
        var (hash, salt) = _passwordsService.HashPasword(user.Password);
        user.Password = hash;
        using var transactionMainerLxContext = _transactionMainerLxRepository.BeginTransaction();
        using var transactionSaltLxContext = _transactionSaltLxRepository.BeginTransaction();
        try
        {
            _logger.Information(UsersServiceLogs.UpdateUserById, userId);
            _usersRepository.UpdateUser(user);

            _logger.Information(UsersServiceLogs.GetSaltByUserId, userId);
            var saltDb = _saltsRepository.GetSaltByUserId(userId);
            saltDb.Salt = salt;
            _logger.Information(UsersServiceLogs.UpdateSalt);
            _saltsRepository.UpdateSalt(saltDb);
            _logger.Information(UsersServiceLogs.CompleteSalt, userId);
            _transactionMainerLxRepository.CommitTransaction(transactionMainerLxContext);
            _transactionSaltLxRepository.CommitTransaction(transactionSaltLxContext);
        }
        catch (Exception ex)
        {
            transactionMainerLxContext.Rollback();
            transactionSaltLxContext.Rollback();
            Log.Error(ex.Message);
        }
    }

    public void UpdateUserMail(Guid userId, UpdateUserMailRequest request)
    {
        _logger.Information(UsersServiceLogs.CheckUserById, userId);
        var user = _usersRepository.GetUserById(userId)
            ?? throw new NotFoundException(string.Format(UsersServiceExceptions.NotFoundException, userId));
        if (_usersRepository.GetUserByMail(request.Mail.ToLower()) is not null)
        {
            throw new ConflictException(UsersServiceExceptions.ConflictException);
        }
        _logger.Information(UsersServiceLogs.UpdateUserMail, userId);
        user.Mail = request.Mail.ToLower();
        _logger.Information(UsersServiceLogs.UpdateUserById, userId);
        _usersRepository.UpdateUser(user);
    }

    public void UpdateUserRole(Guid userId, UpdateUserRoleRequest request)
    {
        _logger.Information(UsersServiceLogs.CheckUserById, userId);
        var user = _usersRepository.GetUserById(userId)
            ?? throw new NotFoundException(string.Format(UsersServiceExceptions.NotFoundException, userId));
        _logger.Information(UsersServiceLogs.UpdateUserRole, userId);
        user.Role = request.Role;
        _logger.Information(UsersServiceLogs.UpdateUserById, userId);
        _usersRepository.UpdateUser(user);
    }

    public void DeleteUserById(Guid id)
    {
        _logger.Information(UsersServiceLogs.CheckUserById, id);
        var user = _usersRepository.GetUserById(id)
            ?? throw new NotFoundException(string.Format(UsersServiceExceptions.NotFoundException, id));
        using var transactionMainerLxContext = _transactionMainerLxRepository.BeginTransaction();
        using var transactionSaltLxContext = _transactionSaltLxRepository.BeginTransaction();
        try
        {
            _logger.Information(UsersServiceLogs.SetIsDeletedUserById, id);
            user.IsDeleted = true;
            _logger.Information(UsersServiceLogs.UpdateUserById, id);
            _usersRepository.UpdateUser(user);

            _logger.Information(UsersServiceLogs.GetSaltByUserId, id);
            var salt = _saltsRepository.GetSaltByUserId(user.Id);
            _logger.Information(UsersServiceLogs.DeleteSalt, user.Id);
            _saltsRepository.DeleteSalt(salt);
            _transactionMainerLxRepository.CommitTransaction(transactionMainerLxContext);
            _transactionSaltLxRepository.CommitTransaction(transactionSaltLxContext);
        }
        catch (Exception ex)
        {
            transactionMainerLxContext.Rollback();
            transactionSaltLxContext.Rollback();
            Log.Error(ex.Message);
        }
    }

    public Guid GetUserIdByOrderId(Guid orderId)
    {
        _logger.Information(UsersServiceLogs.GetUserByOrderId, orderId);
        var order = _ordersRepository.GetOrderById(orderId) ?? throw new NotFoundException(string.Format(OrdersServiceExceptions.NotFoundException, orderId));

        return order.Customer.Id;
    }
}
