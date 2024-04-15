using FirstBackend.Core.Dtos;
using FirstBackend.DataLayer.Repositories;

namespace FirstBackend.Buiseness
{
    public class UsersService
    {
        private readonly UsersRepository _usersRepository;

        public UsersService()
        {
            _usersRepository = new();
        }

        public List<UserDto> GetAllUsers()
        {
            return _usersRepository.GetAllUsers();
        }

        public UserDto GetUserById(Guid id)
        {
            return _usersRepository.GetUserById(id);
        }
    }
}
