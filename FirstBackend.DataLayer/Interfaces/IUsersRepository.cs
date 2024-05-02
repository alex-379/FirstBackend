using FirstBackend.Core.Dtos;

namespace FirstBackend.DataLayer.Interfaces;

public interface IUsersRepository
{
    Guid AddUser(UserDto user);
    List<UserDto> GetAllUsers();
    UserDto GetUserById(Guid id);
    UserDto GetUserByMail(string mail);
    UserDto GetUserByUserName(string userName);
    void UpdateUser(UserDto user);
    void DeleteUser(UserDto user);
}