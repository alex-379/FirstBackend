using FirstBackend.Core.Enums;

namespace FirstBackend.API.Models.Requests;

public class LoginUserRequest
{
    public string Mail { get; set; }
    public string Password { get; set; }
}
