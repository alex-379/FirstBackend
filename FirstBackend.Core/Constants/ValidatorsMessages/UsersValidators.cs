namespace FirstBackend.Core.Constants.ValidatorsMessages;

public static class UsersValidators
{
    public const string Name = "Требуется ввести имя";
    public const string Mail = "Неправильный e-mail";
    public const string Role = "Неправильная роль";
    public const string Password = "Требуется ввести пароль";
    public const string PasswordRule = "Пароль должен быть не менее {0} символов и содержать строчную, прописную латинские буквы, цифру";
}
