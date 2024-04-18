using FirstBackend.Core.Dtos;
using FirstBackend.DataLayer.Interfaces;

namespace FirstBackend.DataLayer.Repositories;

public class UsersRepository : BaseRepository, IUsersRepository
{
    public UsersRepository(MainerLxContext context) : base(context)
    {

    }

    public List<UserDto> GetAllUsers()
    {
        return _ctx.Users.ToList();
    }

    public UserDto GetUserById(Guid id)
    {
        return new()
        {
            Id = id,
            UserName = "Lx",
            Mail = "lx@ya.ru"
        };
    }
}
