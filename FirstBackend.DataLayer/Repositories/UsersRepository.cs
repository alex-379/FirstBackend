using FirstBackend.Core.Dtos;
using FirstBackend.DataLayer.Interfaces;

namespace FirstBackend.DataLayer.Repositories;

public class UsersRepository : IUsersRepository
{
    public UsersRepository()
    {

    }

    public List<UserDto> GetAllUsers()
    {
        return [];
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
