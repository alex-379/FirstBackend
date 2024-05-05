using FirstBackend.Core.Dtos;

namespace FirstBackend.DataLayer.Interfaces;

public interface IUsersRepository
{
    Guid AddUser(UserDto user);
    List<UserDto> GetAllUsers();
    UserDto GetUserById(Guid id);
    UserDto GetUserByMail(string mail);
    UserDto GetUserByName(string name);
    void UpdateUser(UserDto user);
    void DeleteUser(UserDto user);
}