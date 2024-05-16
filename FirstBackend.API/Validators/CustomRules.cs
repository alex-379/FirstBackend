using FirstBackend.Core.Constants;
using FirstBackend.Core.Constants.ValidatorsMessages;
using FluentValidation;
using FluentValidation.Validators;

namespace FirstBackend.API.Validators;

public static class CustomRules
{
    public static IRuleBuilderOptions<T, string> MatchPasswordRule<T>(this IRuleBuilder<T, string> ruleBuilder)
    {
        var passwordRule = ruleBuilder
            .SetValidator(
            new RegularExpressionValidator<T>(
                $"(?=^.{{{ValidationSettings.PasswordLength},}}$)((?=.*\\d)|(?=.*\\W+))(?![.\\n])(?=.*[A-Z])(?=.*[a-z]).*$"
                ))
            .WithMessage(string.Format(UsersValidators.PasswordRule, ValidationSettings.PasswordLength));

        return passwordRule;
    }
}
