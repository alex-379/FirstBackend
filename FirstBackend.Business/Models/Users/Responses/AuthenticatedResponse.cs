namespace FirstBackend.Business.Models.Users.Responses;

public class AuthenticatedResponse
{
    public string AccessToken { get; set; }
    public string RefreshToken { get; set; }
}
