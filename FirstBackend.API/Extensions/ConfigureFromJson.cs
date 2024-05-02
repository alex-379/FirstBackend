using FirstBackend.API.Configuration;
using FirstBackend.Buiseness.Configuration;
using Microsoft.Extensions.Configuration;

namespace FirstBackend.API.Extensions;

public static class ConfigureFromJson
{
    public static void AddConfigurationFromJson(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped(sp => configuration.GetSection("SecretSettings").Get<SecretSettings>(options => options.BindNonPublicProperties = true));
        services.AddScoped(sp => configuration.GetSection("JwtToken").Get<JwtToken>(options => options.BindNonPublicProperties = true));
    }
}
