namespace FirstBackend.API.Extensions;

public static class ConfigureServices
{
    public static void ConfigureApiServices(this IServiceCollection services, ConfigurationManager configurationManager)
    {
        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
        services.AddDataBases(configurationManager);
        services.AddExeptionsHandler();
    }
}
