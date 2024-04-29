namespace FirstBackend.API.Configuration;

public class EnviromentVariables(IConfiguration configuration)
{
    public string Database { get; } = configuration["MainerLx"];
    public string Salt { get; } = configuration["SaltLx"];
    public string SecretPassword { get; } = configuration["SecretLx"];
    public string SecretToken { get; } = configuration["SecretTokenLx"];
}
