using FirstBackend.Core.Dtos;

namespace FirstBackend.DataLayer.Interfaces;

public interface IUsersRepository
{
    List<UserDto> GetAllUsers();
    UserDto GetUserById(Guid id);
}