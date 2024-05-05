using FirstBackend.Buiseness.Models.Tokens.Requests;
using FluentValidation;

namespace FirstBackend.Buiseness.Validators;

public class RefreshTokenValidator : AbstractValidator<RefreshTokenRequest>
{
    public RefreshTokenValidator()
    {
        RuleFor(r => r.AccessToken)
            .NotEmpty()
            .WithMessage("Требуется передать AccessToken");
        RuleFor(r => r.RefreshToken)
            .NotEmpty()
            .WithMessage("Требуется передать RefreshToken");
    }
}
