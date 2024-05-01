using FirstBackend.API.Configuration;
using FirstBackend.Core.Constants;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace FirstBackend.API.Extensions;

public static class ConfigureAuthentication
{
    public static void AddAuthenticationService(this IServiceCollection services, EnviromentVariables enviromentVariables)
    {
        services.AddAuthentication(opt =>
        {
            opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = TokenValidationConstants.ValidIssuer,
                ValidAudience = TokenValidationConstants.ValidAudience,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(enviromentVariables.SecretToken))
            };
        });
    }
}
