using FirstBackend.Core.Dtos;

namespace FirstBackend.DataLayer.Repositories;

public class UsersRepository
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
