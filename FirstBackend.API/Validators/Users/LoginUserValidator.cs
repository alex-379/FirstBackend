using FirstBackend.Business.Models.Users.Requests;
using FirstBackend.Core.Constants.ValidatorsMessages;
using FluentValidation;

namespace FirstBackend.API.Validators.Users;

public class LoginUserValidator : AbstractValidator<LoginUserRequest>
{
    public LoginUserValidator()
    {
        RuleFor(r => r.Mail)
            .NotEmpty()
            .WithMessage(UsersValidators.Mail);
        RuleFor(r => r.Password)
            .NotEmpty()
            .WithMessage(UsersValidators.Mail);
    }
}
