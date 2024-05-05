using FirstBackend.Buiseness.Models.Users.Requests;
using FirstBackend.Core.Constants;
using FluentValidation;

namespace FirstBackend.Buiseness.Validators;

public class UsersValidator : AbstractValidator<CreateUserRequest>
{
    public UsersValidator()
    {
        RuleFor(u => u.Name)
           .NotEmpty()
           .WithMessage("Требуется ввести имя");
        RuleFor(u => u.Mail)
            .EmailAddress()
            .WithMessage("Неправильный e-mail");
        RuleFor(u => u.Password)
            .Length(ValidationSettings.PasswordLength)
            .WithMessage($"Пароль должен быть не менее {ValidationSettings.PasswordLength} символов");
    }
}
