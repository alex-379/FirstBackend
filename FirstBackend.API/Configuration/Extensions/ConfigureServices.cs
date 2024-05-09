using FirstBackend.Buiseness.Models.Users;

namespace FirstBackend.API.Configuration.Extensions;

public static class ConfigureServices
{
    public static void ConfigureApiServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddSwager(configuration);
        services.AddExeptionsHandler();
        services.AddDataBases(configuration);
        services.AddAuthenticationService(configuration);
        services.AddAutoMapper(typeof(UsersMappingProfile).Assembly);
        services.AddValidation();
        services.AddConfigurationFromJson(configuration);
    }
}
