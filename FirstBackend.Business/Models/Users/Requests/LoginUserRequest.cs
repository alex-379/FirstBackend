namespace FirstBackend.Business.Models.Users.Requests;

public class LoginUserRequest
{
    public string Mail { get; set; }
    public string Password { get; set; }
}
