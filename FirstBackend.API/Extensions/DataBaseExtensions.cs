using FirstBackend.DataLayer;
using Microsoft.EntityFrameworkCore;

namespace FirstBackend.API.Extensions;

public static class DataBaseExtensions
{
    public static void ConfigureDataBase(this IServiceCollection services)
    {
        services.AddDbContext<MainerLxContext>(
            options => options
                .UseNpgsql(Environment.GetEnvironmentVariable("MainerLx"))
                .UseSnakeCaseNamingConvention()
        );
    }
}
