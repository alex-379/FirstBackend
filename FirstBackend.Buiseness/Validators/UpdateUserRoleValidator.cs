using FirstBackend.Buiseness.Models.Users.Requests;
using FluentValidation;

namespace FirstBackend.Buiseness.Validators;

public class UpdateUserRoleValidator : AbstractValidator<UpdateUserRoleRequest>
{
    public UpdateUserRoleValidator()
    {
        RuleFor(r => r.Role)
            .IsInEnum()
            .WithMessage("Неправильная роль");
    }
}
