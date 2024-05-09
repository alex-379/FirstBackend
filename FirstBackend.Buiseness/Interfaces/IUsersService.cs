using FirstBackend.Buiseness.Models.Users.Requests;
using FirstBackend.Buiseness.Models.Users.Responses;
using Microsoft.AspNetCore.Http;

namespace FirstBackend.Buiseness.Interfaces;

public interface IUsersService
{
    Guid AddUser(CreateUserRequest request);
    AuthenticatedResponse LoginUser(LoginUserRequest request);
    List<UserResponse> GetAllUsers();
    UserFullResponse GetUserById(Guid id);
    void UpdateUser(Guid userId, UpdateUserDataRequest request);
    void DeleteUserById(Guid id);
    void UpdateUserPassword(Guid userId, UpdateUserPasswordRequest request);
    void UpdateUserMail(Guid userId, UpdateUserMailRequest request);
    void CheckUserRights(Guid id, HttpContext httpContext);
    Guid GetUserIdByOrderId(Guid orderId);
}