using FirstBackend.Buiseness.Models.Users.Requests;
using FirstBackend.Core.Constants;
using FluentValidation;

namespace FirstBackend.Buiseness.Validators;

public class UpdateUserPasswordValidator : AbstractValidator<UpdateUserPasswordRequest>
{
    public UpdateUserPasswordValidator()
    {
        RuleFor(r => r.Password)
            .MinimumLength(ValidationSettings.PasswordLength)
            .WithMessage($"Пароль должен быть не менее {ValidationSettings.PasswordLength} символов");
    }
}
