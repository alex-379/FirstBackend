using FirstBackend.API.Configuration;
using FirstBackend.DataLayer;
using Microsoft.EntityFrameworkCore;
namespace FirstBackend.API.Extensions;

public static class ConfigureDataBases
{
    public static void AddDataBases(this IServiceCollection services, EnviromentVariables enviromentVariables)
    {
        services.AddDbContext<MainerLxContext>(
            options => options
                .UseNpgsql(enviromentVariables.Database)
                .UseSnakeCaseNamingConvention()
        );

        services.AddDbContext<SaltLxContext>(
            options => options
                .UseNpgsql(enviromentVariables.Salt)
                .UseSnakeCaseNamingConvention()
        );
    }
}
