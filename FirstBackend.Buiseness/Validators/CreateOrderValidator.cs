using FirstBackend.Buiseness.Models.Orders.Requests;
using FluentValidation;

namespace FirstBackend.Buiseness.Validators;

public class CreateOrderValidator : AbstractValidator<CreateOrderRequest>
{
    public CreateOrderValidator()
    {
        RuleFor(r => r.Description)
            .NotEmpty()
            .WithMessage("Требуется ввести описание заказа");
    }
}
