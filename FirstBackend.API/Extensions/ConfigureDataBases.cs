using FirstBackend.DataLayer.Contexts;
using Microsoft.EntityFrameworkCore;
namespace FirstBackend.API.Extensions;

public static class ConfigureDataBases
{
    public static void AddDataBases(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<MainerLxContext>(
            options => options
                .UseNpgsql(configuration.GetConnectionString("MainerLxDb"))
                .UseSnakeCaseNamingConvention()
        );

        services.AddDbContext<SaltLxContext>(
            options => options
                .UseNpgsql(configuration.GetConnectionString("SaltLxDb"))
                .UseSnakeCaseNamingConvention()
        );
    }
}
