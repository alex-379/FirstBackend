using FirstBackend.Core.Enums;

namespace FirstBackend.Business.Models.Users.Requests;

public class UpdateUserRoleRequest
{
    public UserRole Role { get; set; }
}
