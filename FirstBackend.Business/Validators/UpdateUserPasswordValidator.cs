using FirstBackend.Business.Models.Users.Requests;
using FirstBackend.Core.Constants;
using FluentValidation;

namespace FirstBackend.Business.Validators;

public class UpdateUserPasswordValidator : AbstractValidator<UpdateUserPasswordRequest>
{
    public UpdateUserPasswordValidator()
    {
        RuleFor(r => r.Password)
            .MinimumLength(ValidationSettings.PasswordLength)
            .WithMessage($"Пароль должен быть не менее {ValidationSettings.PasswordLength} символов");
    }
}
