using FirstBackend.DataLayer.Contexts;
using Microsoft.EntityFrameworkCore;

namespace FirstBackend.API.Configuration.Extensions;

public static class ConfigureDataBases
{
    public static void AddDataBases(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<MainerLxContext>(
            options => options
                .UseNpgsql(configuration["DatabaseSettings:MainerLxDb"])
                .UseSnakeCaseNamingConvention()
        );

        services.AddDbContext<SaltLxContext>(
            options => options
                .UseNpgsql(configuration["DatabaseSettings:SaltLxDb"])
                .UseSnakeCaseNamingConvention()
        );
    }
}
