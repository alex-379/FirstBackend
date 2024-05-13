using FirstBackend.Business.Models.Users.Requests;
using FirstBackend.Core.Constants.ValidatorsMessages;
using FluentValidation;

namespace FirstBackend.Business.Validators.Users;

public class UpdateUserMailValidator : AbstractValidator<UpdateUserMailRequest>
{
    public UpdateUserMailValidator()
    {
        RuleFor(r => r.Mail)
            .EmailAddress()
            .WithMessage(UsersValidators.Mail);
    }
}