using FirstBackend.Business.Models.Users.Requests;
using FirstBackend.Business.Models.Users.Responses;

namespace FirstBackend.Business.Interfaces;

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
    void UpdateUserRole(Guid userId, UpdateUserRoleRequest request);
    Guid GetUserIdByOrderId(Guid orderId);
}