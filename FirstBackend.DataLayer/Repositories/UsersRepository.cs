using FirstBackend.Core.Dtos;
using FirstBackend.DataLayer.Interfaces;
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

        return [.. _ctx.Users];
    }

    public UserDto GetUserById(Guid id)
    {
        _logger.Information("Идём в базу данных и ищем пользователя по ID {id}", id);

        return _ctx.Users.FirstOrDefault(u => u.Id == id);
    }

    public void UpdateUser(UserDto user)
    {
        _logger.Information("Идём в базу данных и обновляем пользователя с ID {id}", user.Id);
        _ctx.Users.Update(user);
        _ctx.SaveChanges();
    } 

    public void DeleteUser(UserDto user)
    {
        _logger.Information("Идём в базу данных и удаляем пользователя с ID {id}", user.Id);
        _ctx.Users.Remove(user);
        _ctx.SaveChanges();
    }
}
