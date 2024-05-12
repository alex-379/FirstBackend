using FirstBackend.Core.Constants.Logs.DataLayer;
using FirstBackend.Core.Dtos;
using FirstBackend.DataLayer.Contexts;
using FirstBackend.DataLayer.Interfaces;
using Serilog;

namespace FirstBackend.DataLayer.Repositories;

public class UsersRepository(MainerLxContext context) : BaseRepository<MainerLxContext>(context), IUsersRepository
{
    private readonly ILogger _logger = Log.ForContext<UsersRepository>();

    public Guid AddUser(UserDto user)
    {
        _ctx.Users.Add(user);
        _ctx.SaveChanges();
        _logger.Information(UsersRepositoryLogs.AddUser, user.Id);

        return user.Id;
    }

    public IEnumerable<UserDto> GetAllUsers()
    {
        _logger.Information(UsersRepositoryLogs.GetAllUsers);

        return _ctx.Users
            .Where(u=>!u.IsDeleted);
    }

    public UserDto GetUserById(Guid id)
    {
        _logger.Information(UsersRepositoryLogs.GetUserById, id);

        return _ctx.Users
            .FirstOrDefault(u => u.Id == id
                && !u.IsDeleted);
    }

    public UserDto GetUserByMail(string mail)
    {
        _logger.Information(UsersRepositoryLogs.GetUserByMail, mail);

        return _ctx.Users
            .FirstOrDefault(u => u.Mail == mail
                && !u.IsDeleted);
    }

    public UserDto GetUserByOrderId(Guid orderId)
    {
        _logger.Information(UsersRepositoryLogs.GetUserByOrderId, orderId);

        return _ctx.Users
            .FirstOrDefault(u => u.Orders
            .Any(o => o.Id == orderId));
    }

    public void UpdateUser(UserDto user)
    {
        _logger.Information(UsersRepositoryLogs.UpdateUser, user.Id);
        _ctx.Users.Update(user);
        _ctx.SaveChanges();
    }
}
