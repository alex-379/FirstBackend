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
        _logger.Information($"Проверяем соответствует ли пароль требуемой длине");
        if (string.IsNullOrEmpty(user.Password) || user.Password.Length < 8) 
        {
            throw new ValidationException("Пароль должен быть не менее 8 символов");
        }

        _usersRepository.AddUser(user);
        _logger.Information($"Обращаемся к методу репозитория Создание нового пользователя с ID {user.Id}");

        return user.Id;
    }

    public List<UserDto> GetAllUsers()
    {
        _logger.Information($"Обращаемся к методу репозитория Получение всех пользователей");

        return _usersRepository.GetAllUsers();
    } 

    public UserDto GetUserById(Guid id)
    {
        _logger.Information($"Обращаемся к методу репозитория Получение пользователя по ID {id}");
        return _usersRepository.GetUserById(id);
    }

    public void UpdateUser(UserDto user)
    {
        _logger.Information($"Проверяем существует ли пользователь с ID {user.Id}");
        user = _usersRepository.GetUserById(user.Id) ?? throw new NotFoundException($"Пользователь с ID {user.Id} не найден");
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
