using FirstBackend.Business.Validators;
using FluentValidation;
using SharpGrip.FluentValidation.AutoValidation.Mvc.Extensions;

namespace FirstBackend.API.Configuration.Extensions;

public static class CofigureValidation
{
    public static void AddValidation(this IServiceCollection services)
    {
        services.AddFluentValidationAutoValidation(configuration =>
        {
            configuration.OverrideDefaultResultFactoryWith<ValidationResultFactory>();
        });
        services.AddValidatorsFromAssemblyContaining<CreateUserValidator>();
    }
}
