using FirstBackend.Core.Dtos;

namespace FirstBackend.Buiseness.Interfaces;

public interface IUsersService
{
    Guid AddUser(UserDto user);
    List<UserDto> GetAllUsers();
    UserDto GetUserById(Guid id);
    void UpdateUser(Guid userId, UserDto userUpdate);
    void DeleteUserById(Guid id);
}