using FirstBackend.Core.Dtos;

namespace FirstBackend.API.Models.Requests;

public class CreateUserRequest
{
    public string UserName { get; set; }
    public string Mail { get; set; }
    public string Password { get; set; }
}
