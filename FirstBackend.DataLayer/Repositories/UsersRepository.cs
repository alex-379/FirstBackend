using FirstBackend.Core.Dtos;
using FirstBackend.DataLayer.Interfaces;

namespace FirstBackend.DataLayer.Repositories;

public class UsersRepository : BaseRepository, IUsersRepository
{
    public UsersRepository(MainerLxContext context) : base(context)
    {

    }

    public Guid CreateUser(UserDto user)
    {
        _ctx.Users.Add(user);

        return user.Id;
    }

    public List<UserDto> GetAllUsers() => [.. _ctx.Users];

    public UserDto GetUserById(Guid id) => _ctx.Users.FirstOrDefault(u => u.Id == id);

    public void UpdateUser(UserDto user) => _ctx.Users.Update(user);

    public void DeleteUser(UserDto user) => _ctx.Users.Remove(user);
}
