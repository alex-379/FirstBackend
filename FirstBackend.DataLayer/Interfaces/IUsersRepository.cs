using FirstBackend.Core.Dtos;

namespace FirstBackend.DataLayer.Interfaces;

public interface IUsersRepository
{
    Guid CreateUser(UserDto user);
    List<UserDto> GetAllUsers();
    UserDto GetUserById(Guid id);
    void UpdateUser(UserDto user);
    void DeleteUser(UserDto user);
}