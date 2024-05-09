using Microsoft.OpenApi.Models;

namespace FirstBackend.API.Configuration.Extensions;

public static class ConfigureSwager
{
    public static void AddSwager(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSwaggerGen(opt =>
        {
            opt.SwaggerDoc(configuration["OpenApiSettings:Version"],
                new OpenApiInfo { Title = configuration["OpenApiSettings:Title"],
                                  Version = configuration["OpenApiSettings:Version"] 
                });

            opt.AddSecurityDefinition(configuration["OpenApiSettings:SecurityScheme"], new OpenApiSecurityScheme
            {
                In = ParameterLocation.Header,
                Description = configuration["OpenApiSettings:Description"],
                Name = configuration["OpenApiSettings:Name"],
                Type = SecuritySchemeType.Http,
                BearerFormat = configuration["OpenApiSettings:BearerFormat"],
                Scheme = configuration["OpenApiSettings:SecurityScheme"]
            });

            opt.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type=ReferenceType.SecurityScheme,
                        Id=configuration["OpenApiSettings:SecurityScheme"]
                    }
                },
                Array.Empty<string>()
                }
            });
        });
    }
}