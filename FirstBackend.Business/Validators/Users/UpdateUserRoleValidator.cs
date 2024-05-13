using FirstBackend.Business.Models.Users.Requests;
using FirstBackend.Core.Constants.ValidatorsMessages;
using FluentValidation;

namespace FirstBackend.Business.Validators.Users;

public class UpdateUserRoleValidator : AbstractValidator<UpdateUserRoleRequest>
{
    public UpdateUserRoleValidator()
    {
        RuleFor(r => r.Role)
            .IsInEnum()
            .WithMessage(UsersValidators.Role);
    }
}
