using FirstBackend.Buiseness.Interfaces;
using FirstBackend.Core.Dtos;
using FirstBackend.Core.Exeptions;
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

    public void DeleteUserById(Guid id)
    {
        var user = _usersRepository.GetUserById(id) ?? throw new NotFoundException($"Пользователь с ID {id} не найден");
        _usersRepository.DeleteUser(user);
    }
}
