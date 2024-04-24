using FirstBackend.Buiseness.Interfaces;
using FirstBackend.Core.Dtos;
using FirstBackend.Core.Exeptions;
using FirstBackend.DataLayer.Interfaces;
using Serilog;

namespace FirstBackend.Buiseness.Services;

public class UsersService : IUsersService
{
    private readonly IUsersRepository _usersRepository;
    private readonly ILogger _logger = Log.ForContext<UsersService>();

    public UsersService(IUsersRepository usersRepository)
    {
        _usersRepository = usersRepository;
    }

    public Guid AddUser (UserDto user)
    {
        if (string.IsNullOrEmpty(user.Password) || user.Password.Length < 8) 
        {
            throw new ValidationException("Пароль должен быть не менее 8 символов");
        }

        return Guid.NewGuid ();
    }

    public List<UserDto> GetAllUsers() => _usersRepository.GetAllUsers();

    public UserDto GetUserById(Guid id)
    {
        _logger.Information($"Обращаемся к методу репозитория Получение пользователя по ID {id}");
        return _usersRepository.GetUserById(id);
    }

    public void DeleteUserById(Guid id)
    {
        var user = _usersRepository.GetUserById(id) ?? throw new NotFoundException($"Пользователь с ID {id} не найден");
        _usersRepository.DeleteUser(user);
    }
}
