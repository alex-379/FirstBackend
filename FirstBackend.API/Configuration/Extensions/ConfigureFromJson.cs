﻿using FirstBackend.Business.Configuration;
using FirstBackend.Core.Constants;

namespace FirstBackend.API.Configuration.Extensions;

public static class ConfigureFromJson
{
    public static void AddConfigurationFromJson(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped(sp => configuration.GetSection(ConfigurationSettings.SecretSettings).Get<SecretSettings>(options => options.BindNonPublicProperties = true));
        services.AddScoped(sp => configuration.GetSection(ConfigurationSettings.JwtToken).Get<JwtToken>(options => options.BindNonPublicProperties = true));
    }
}
