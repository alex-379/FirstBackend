using FirstBackend.Buiseness.Interfaces;
using FirstBackend.Core.Dtos;
using FirstBackend.DataLayer.Interfaces;

namespace FirstBackend.Buiseness.Services;

public class UsersService : IUsersService
{
    private readonly IUsersRepository _usersRepository;

    public UsersService(IUsersRepository usersRepository)
    {
        _usersRepository = usersRepository;
    }

    public List<UserDto> GetAllUsers() => _usersRepository.GetAllUsers();

    public UserDto GetUserById(Guid id) => _usersRepository.GetUserById(id);
}
