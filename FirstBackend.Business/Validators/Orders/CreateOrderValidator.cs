using FirstBackend.Business.Models.Orders.Requests;
using FirstBackend.Core.Constants.ValidatorsMessages;
using FluentValidation;

namespace FirstBackend.Business.Validators.Orders;

public class CreateOrderValidator : AbstractValidator<CreateOrderRequest>
{
    public CreateOrderValidator()
    {
        RuleFor(r => r.Description)
            .NotEmpty()
            .WithMessage(OrdersValidators.Description);
        RuleFor(r => r.Devices)
            .NotEmpty()
            .WithMessage(OrdersValidators.Devices);
    }
}
