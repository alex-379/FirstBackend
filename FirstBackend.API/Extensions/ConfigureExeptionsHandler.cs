using FirstBackend.API.Configuration;

namespace FirstBackend.API.Extensions;

public static class ConfigureExeptionsHandler
{
    public static void AddExeptionsHandler(this IServiceCollection services)
    {
        services.AddExceptionHandler<GlobalExceptionHandler>();
        services.AddProblemDetails();
    }
}
