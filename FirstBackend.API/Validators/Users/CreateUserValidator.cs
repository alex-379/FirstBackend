using FirstBackend.Business.Models.Users.Requests;
using FirstBackend.Core.Constants.ValidatorsMessages;
using FluentValidation;

namespace FirstBackend.API.Validators.Users;

public class CreateUserValidator : AbstractValidator<CreateUserRequest>
{
    public CreateUserValidator()
    {
        RuleFor(r => r.Name)
           .NotEmpty()
           .WithMessage(UsersValidators.Name);
        RuleFor(r => r.Mail)
            .EmailAddress()
            .WithMessage(UsersValidators.Mail);
        RuleFor(r => r.Password)
            .MatchPasswordRule();
    }
}
