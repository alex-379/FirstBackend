using FirstBackend.Business.Models.Tokens.Requests;
using FluentValidation;

namespace FirstBackend.Business.Validators;

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
