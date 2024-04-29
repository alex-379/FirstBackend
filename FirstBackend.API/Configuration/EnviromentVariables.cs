namespace FirstBackend.API.Configuration;

public class EnviromentVariables(IConfiguration configuration)
{
    public string Database { get; private set; } = configuration["MainerLx"];
    public string Salt { get; private set; } = configuration["SaltLx"];
    public string SecretPassword { get; private set; } = configuration["SecretLx"];
}
