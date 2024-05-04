﻿using FirstBackend.Buiseness.Models.Users;
using FirstBackend.Buiseness.Validators;
using FluentValidation;
using SharpGrip.FluentValidation.AutoValidation.Endpoints.Extensions;

namespace FirstBackend.API.Extensions;

public static class ConfigureServices
{
    public static void ConfigureApiServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddSwager();
        services.AddExeptionsHandler();
        services.AddDataBases(configuration);
        services.AddAuthenticationService(configuration);
        services.AddAutoMapper(typeof(UsersMappingProfile).Assembly);
        //services.AddFluentValidationAutoValidation();
        services.AddValidatorsFromAssemblyContaining<UsersValidator>();
        services.AddConfigurationFromJson(configuration);
    }
}
