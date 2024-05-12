using FirstBackend.Business.Models.Users.Requests;
using FluentValidation;

namespace FirstBackend.Business.Validators;

public class UpdateUserRoleValidator : AbstractValidator<UpdateUserRoleRequest>
{
    public UpdateUserRoleValidator()
    {
        RuleFor(r => r.Role)
            .IsInEnum()
            .WithMessage("Неправильная роль");
    }
}
