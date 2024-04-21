using FirstBackend.Core.Dtos;
using FirstBackend.DataLayer.Interfaces;

namespace FirstBackend.DataLayer.Repositories;

public class UsersRepository : BaseRepository, IUsersRepository
{
    public UsersRepository(MainerLxContext context) : base(context)
    {

    }

    //public List<UserDto> AddUser()
    //{
    //    return _ctx.Users.Add();
    //}

    public List<UserDto> GetAllUsers() => [.. _ctx.Users];

    public UserDto GetUserById(Guid id) => _ctx.Users.FirstOrDefault(u => u.Id == id);
}
