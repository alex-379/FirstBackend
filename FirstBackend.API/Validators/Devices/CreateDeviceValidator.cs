using FirstBackend.Business.Models.Devices.Requests;
using FirstBackend.Core.Constants.ValidatorsMessages;
using FluentValidation;

namespace FirstBackend.API.Validators.Devices;

public class CreateDeviceValidator : AbstractValidator<CreateDeviceRequest>
{
    public CreateDeviceValidator()
    {
        RuleFor(r => r.Name)
            .NotEmpty()
            .WithMessage(DevicesValidators.Name);
        RuleFor(r => r.Type)
            .IsInEnum()
            .WithMessage(DevicesValidators.Type);
        RuleFor(r => r.Address)
            .NotEmpty()
            .WithMessage(DevicesValidators.Address);
    }
}
