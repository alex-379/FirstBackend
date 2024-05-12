using FirstBackend.Business.Models.Users.Requests;
using FluentValidation;

namespace FirstBackend.Business.Validators;

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
