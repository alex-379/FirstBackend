namespace FirstBackend.Buiseness.Models.Users.Requests;

public class CreateUserRequest
{
    public string UserName { get; set; }
    public string Mail { get; set; }
    public string Password { get; set; }
}
