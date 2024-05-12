using FirstBackend.Business.Models.Users.Requests;
using FluentValidation;

namespace FirstBackend.Business.Validators;

public class CreateUserValidator : AbstractValidator<CreateUserRequest>
{
    public CreateUserValidator()
    {
        RuleFor(r => r.Name)
           .NotEmpty()
           .WithMessage("Требуется ввести имя");
        RuleFor(r => r.Mail)
            .EmailAddress()
            .WithMessage("Неправильный e-mail");
        RuleFor(r => r.Password)
            .MatchPasswordRule();
    }
}
