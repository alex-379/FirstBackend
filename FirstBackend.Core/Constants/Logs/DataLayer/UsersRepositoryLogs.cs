namespace FirstBackend.Core.Constants.Logs.DataLayer;

public static class UsersRepositoryLogs
{
    public const string AddUser = "Вносим в базу данных пользователя с ID {id}";
    public const string GetUsers = "Идём в базу данных и ищем всех пользователей";
    public const string GetUserById = "Идём в базу данных и ищем пользователя по ID {id}";
    public const string GetUserByMail = "Идём в базу данных и ищем пользователя по почте {mail}";
    public const string UpdateUser = "Идём в базу данных и обновляем пользователя с ID {id}";
}
