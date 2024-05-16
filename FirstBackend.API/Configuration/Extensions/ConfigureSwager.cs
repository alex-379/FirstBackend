﻿using FirstBackend.Core.Constants;
using Microsoft.OpenApi.Models;

namespace FirstBackend.API.Configuration.Extensions;

public static class ConfigureSwager
{
    public static void AddSwager(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSwaggerGen(opt =>
        {
            opt.SwaggerDoc(configuration[ConfigurationSettings.OpenApiVersion],
                new OpenApiInfo
                {
                    Title = configuration[ConfigurationSettings.OpenApiTitle],
                    Version = configuration[ConfigurationSettings.OpenApiVersion]
                });

            opt.AddSecurityDefinition(configuration[ConfigurationSettings.OpenApiSecurityScheme], new OpenApiSecurityScheme
            {
                In = ParameterLocation.Header,
                Description = configuration[ConfigurationSettings.OpenApiDescription],
                Name = configuration[ConfigurationSettings.OpenApiName],
                Type = SecuritySchemeType.Http,
                BearerFormat = configuration[ConfigurationSettings.OpenApiBearerFormat],
                Scheme = configuration[ConfigurationSettings.OpenApiSecurityScheme]
            });

            opt.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type=ReferenceType.SecurityScheme,
                        Id=configuration[ConfigurationSettings.OpenApiSecurityScheme]
                    }
                },
                Array.Empty<string>()
                }
            });
        });
    }
}