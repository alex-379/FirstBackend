using FirstBackend.API.Configuration;
using FirstBackend.Buiseness.Models.Devices;
using FluentValidation;

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
        services.AddAutoMapper(typeof(DevicesMappingProfile).Assembly);
        //services.AddValidatorsFromAssemblyContaining<UserValidator>();
    }
}
