using FirstBackend.Business.Models.Users.Requests;
using FirstBackend.Core.Constants.ValidatorsMessages;
using FluentValidation;

namespace FirstBackend.Business.Validators.Users;

public class UpdateUserDataValidator : AbstractValidator<UpdateUserDataRequest>
{
    public UpdateUserDataValidator()
    {
        RuleFor(r => r.Name)
           .NotEmpty()
           .WithMessage(UsersValidators.Name);
    }
}