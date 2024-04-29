using FirstBackend.API.Configuration;
using Microsoft.OpenApi.Models;

namespace FirstBackend.API.Extensions;

public static class ConfigureServices
{
    public static void ConfigureApiServices(this IServiceCollection services, EnviromentVariables enviromentVariables)
    {
        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddSwager();
        services.AddExeptionsHandler();
        services.AddScoped<EnviromentVariables>();
        services.AddDataBases(enviromentVariables);
        services.AddAuthenticationService(enviromentVariables);
    }
}
