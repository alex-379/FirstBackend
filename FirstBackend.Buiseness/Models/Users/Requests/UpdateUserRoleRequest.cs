using FirstBackend.Core.Enums;

namespace FirstBackend.Buiseness.Models.Users.Requests;

public class UpdateUserRoleRequest
{
    public UserRole Role { get; set; }
}
