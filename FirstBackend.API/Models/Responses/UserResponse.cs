namespace FirstBackend.API.Models.Responses;

public class UserResponse
{
    public Guid Id { get; set; }
    public string UserName { get; set; }
    public string Mail { get; set; }
    public string Password { get; set; }
}
