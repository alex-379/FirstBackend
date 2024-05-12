using FirstBackend.Business.Models.Orders.Requests;
using FluentValidation;

namespace FirstBackend.Business.Validators;

public class CreateOrderValidator : AbstractValidator<CreateOrderRequest>
{
    public CreateOrderValidator()
    {
        RuleFor(r => r.Description)
            .NotEmpty()
            .WithMessage("Требуется ввести описание заказа");
    }
}
