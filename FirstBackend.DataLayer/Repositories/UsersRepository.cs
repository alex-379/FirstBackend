using FirstBackend.Core.Dtos;
using FirstBackend.DataLayer.Contexts;
using FirstBackend.DataLayer.Interfaces;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace FirstBackend.DataLayer.Repositories;

public class UsersRepository(MainerLxContext context) : BaseRepository(context), IUsersRepository
{
    private readonly ILogger _logger = Log.ForContext<UsersRepository>();

    public Guid AddUser(UserDto user)
    {
        _ctx.Users.Add(user);
        _ctx.SaveChanges();
        _logger.Information("Вносим в базу данных пользователя с ID {id}", user.Id);

        return user.Id;
    }

    public List<UserDto> GetAllUsers()
    {
        _logger.Information("Идём в базу данных и ищем всех пользователей");

        return [.. _ctx.Users
            .Where(u=>!u.IsDeleted)];
    }

    public UserDto GetUserById(Guid id)
    {
        _logger.Information("Идём в базу данных и ищем пользователя по ID {id}", id);

        return _ctx.Users
            .FirstOrDefault(u => u.Id == id 
                && !u.IsDeleted);
    }

    public UserDto GetUserByMail(string mail)
    {
        _logger.Information("Идём в базу данных и ищем пользователя по почте {mail}", mail);

        return _ctx.Users
            .FirstOrDefault(u => u.Mail == mail
                && !u.IsDeleted);
    }

    public UserDto GetUserByOrderId(Guid orderId)
    {
        _logger.Information("Идём в базу данных и ищем пользователя по ID заказа {orderId}", orderId);

        return _ctx.Users
            .FirstOrDefault(u => u.Orders
            .Any(o => o.Id == orderId));
    }

    public void UpdateUser(UserDto user)
    {
        _logger.Information("Идём в базу данных и обновляем пользователя с ID {id}", user.Id);
        _ctx.Users.Update(user);
        _ctx.SaveChanges();
    }
}
