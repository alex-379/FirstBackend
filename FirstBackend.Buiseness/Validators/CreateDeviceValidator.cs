using FirstBackend.Buiseness.Models.Devices.Requests;
using FluentValidation;

namespace FirstBackend.Buiseness.Validators;

public class CreateDeviceValidator : AbstractValidator<CreateDeviceRequest>
{
    public CreateDeviceValidator()
    {
        RuleFor(r => r.Name)
            .NotEmpty()
            .WithMessage("Требуется ввести имя устройства");
        RuleFor(r => r.Type)
            .IsInEnum()
            .WithMessage("Неправильный тип устройства");
        RuleFor(r => r.Address)
            .NotEmpty()
            .WithMessage("Требуется ввести адрес устройства");
    }
}
