namespace FirstBackend.API.Configuration;

public class EnviromentVariables(IConfiguration configuration)
{
    public string Database { get; private set; } = configuration[("MainerLxDocker")];
    public string Salt { get; private set; } = configuration[("SaltLxDocker")];
    public string SecretPassword { get; private set; } = configuration[("SecretLx")];
}
