﻿using FirstBackend.Buiseness.Interfaces;
using FirstBackend.Core.Dtos;
using FirstBackend.Core.Exeptions;
using FirstBackend.DataLayer.Interfaces;
using Serilog;

namespace FirstBackend.Buiseness.Services;

public class UsersService(IUsersRepository usersRepository, ISaltsRepository saltsRepository, IPasswordsService passwordsService) : IUsersService
{
    private readonly IUsersRepository _usersRepository = usersRepository;
    private readonly ISaltsRepository _saltsRepository = saltsRepository;
    private readonly IPasswordsService _passwordsService = passwordsService;
    private readonly ILogger _logger = Log.ForContext<UsersService>();

    public Guid AddUser(UserDto user)
    {
        _logger.Information($"Проверяем соответствует ли пароль требуемой длине");
        if (string.IsNullOrEmpty(user.Password) || user.Password.Length < 8)
        {
            throw new ValidationException("Пароль должен быть не менее 8 символов");
        }

        _logger.Information($"Проверяем был ли зарегестрирован пользователь с такой почтой ранее");
        if (_usersRepository.GetAllUsers().Any(u => u.Mail == user.Mail))
        {
            throw new ValidationException("Такой e-mail уже существует");
        }

        user.Password = _passwordsService.HashPasword(user.Password, out var salt);
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
