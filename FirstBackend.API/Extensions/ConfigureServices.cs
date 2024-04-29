using FirstBackend.API.Configuration;

namespace FirstBackend.API.Extensions;

public static class ConfigureServices
{
    public static void ConfigureApiServices(this IServiceCollection services, EnviromentVariables enviromentVariables)
    {
        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
        services.AddExeptionsHandler();
        services.AddScoped<EnviromentVariables>();
        services.AddDataBases(enviromentVariables);
        services.AddAuthenticationService(enviromentVariables);
    }
}
