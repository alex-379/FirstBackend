using FirstBackend.DataLayer;
using Microsoft.EntityFrameworkCore;
namespace FirstBackend.API.Extensions;

public static class ConfigureDataBases
{
    public static void AddDataBases(this IServiceCollection services, ConfigurationManager configurationManager)
    {
        services.AddDbContext<MainerLxContext>(
            options => options
                .UseNpgsql(configurationManager["MainerLxDocker"])
                .UseSnakeCaseNamingConvention()
        );

        services.AddDbContext<SaltLxContext>(
            options => options
                .UseNpgsql(configurationManager["SaltLxDocker"])
                .UseSnakeCaseNamingConvention()
        );
    }
}
