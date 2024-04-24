using FirstBackend.Core.Dtos;
using FirstBackend.DataLayer.Interfaces;
using Serilog;

namespace FirstBackend.DataLayer.Repositories;

public class UsersRepository : BaseRepository, IUsersRepository
{
    private readonly ILogger _logger = Log.ForContext<UsersRepository>();

    public UsersRepository(MainerLxContext context) : base(context)
    {

    }

    public Guid CreateUser(UserDto user)
    {
        _ctx.Users.Add(user);

        return user.Id;
    }

    public List<UserDto> GetAllUsers() => [.. _ctx.Users];

    public UserDto GetUserById(Guid id)
    {
        _logger.Information("Идём в базу данных и ищем пользователя по ID {id}", id);
        return _ctx.Users.FirstOrDefault(u => u.Id == id);
    }


    public void UpdateUser(UserDto user) => _ctx.Users.Update(user);

    public void DeleteUser(UserDto user) => _ctx.Users.Remove(user);
}
