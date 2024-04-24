using FirstBackend.Core.Dtos;

namespace FirstBackend.Buiseness.Interfaces;

public interface IUsersService
{
    List<UserDto> GetAllUsers();
    UserDto GetUserById(Guid id);
    void DeleteUserById(Guid id);
    Guid AddUser(UserDto user);
}