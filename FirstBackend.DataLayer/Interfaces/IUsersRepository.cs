using FirstBackend.Core.Dtos;
using Microsoft.EntityFrameworkCore.Storage;

namespace FirstBackend.DataLayer.Interfaces;

public interface IUsersRepository
{
    Guid AddUser(UserDto user);
    IEnumerable<UserDto> GetUsers();
    UserDto GetUserById(Guid id);
    UserDto GetUserByMail(string mail);
    void UpdateUser(UserDto user);
}