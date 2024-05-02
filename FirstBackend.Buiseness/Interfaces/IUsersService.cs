using FirstBackend.Buiseness.Models.Users.Requests;
using FirstBackend.Buiseness.Models.Users.Responses;

namespace FirstBackend.Buiseness.Interfaces;

public interface IUsersService
{
    Guid AddUser(CreateUserRequest request);
    AuthenticatedResponse LoginUser(LoginUserRequest request);
    List<UserResponse> GetAllUsers();
    UserFullResponse GetUserById(Guid id);
    void UpdateUser(Guid userId, UpdateUserDataRequest request);
    void DeleteUserById(Guid id);
    AuthenticatedResponse UpdateUserPassword(Guid userId, UpdateUserPasswordRequest request, string accessToken);
    void UpdateUserMail(Guid userId, UpdateUserMailRequest request);
}