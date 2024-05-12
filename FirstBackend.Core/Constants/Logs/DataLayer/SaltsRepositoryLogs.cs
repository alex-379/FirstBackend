namespace FirstBackend.Core.Constants.Logs.DataLayer;

public static class SaltsRepositoryLogs
{
    public const string AddSalt = "Вносим в базу данных соль пользователя с ID {id}";
    public const string GetSaltByUserId = "Идём в базу данных и ищем соль по ID пользователя {userId}";
    public const string UpdateSalt = "Обновляем в базе данных соль пользователя с ID {id}";
    public const string DeleteSalt = "Удаляем из базы данных соль пользователя с ID {id}";
}
