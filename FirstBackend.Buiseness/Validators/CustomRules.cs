using FirstBackend.Core.Constants;
using FluentValidation;
using FluentValidation.Validators;

namespace FirstBackend.Buiseness.Validators;

public static class CustomRules
{
    public static IRuleBuilderOptions<T, string> MatchPasswordRule<T>(this IRuleBuilder<T, string> ruleBuilder)
    {
        return ruleBuilder.SetValidator(new RegularExpressionValidator<T>($"(?=^.{{{ValidationSettings.PasswordLength},}}$)((?=.*\\d)|(?=.*\\W+))(?![.\\n])(?=.*[A-Z])(?=.*[a-z]).*$"))
            .WithMessage($"Пароль должен быть не менее {ValidationSettings.PasswordLength} символов и содержать строчную, прописную латинские буквы, цифру");
    }
}
