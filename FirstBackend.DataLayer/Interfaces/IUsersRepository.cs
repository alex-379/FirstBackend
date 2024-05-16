using FirstBackend.Core.Dtos;

namespace FirstBackend.DataLayer.Interfaces;

public interface IUsersRepository
{
    Guid AddUser(UserDto user);
    IEnumerable<UserDto> GetUsers();
    UserDto GetUserById(Guid id);
    UserDto GetUserByMail(string mail);
    void UpdateUser(UserDto user);
}