using FirstBackend.Buiseness.Models.Users.Requests;
using FluentValidation;

namespace FirstBackend.Buiseness.Validators;

public class LoginUserValidator : AbstractValidator<LoginUserRequest>
{
    public LoginUserValidator()
    {
        RuleFor(r => r.Mail)
            .NotEmpty()
            .WithMessage("Требуется ввести e-mail");
        RuleFor(r => r.Password)
            .NotEmpty()
            .WithMessage("Требуется ввести пароль");
    }
}
