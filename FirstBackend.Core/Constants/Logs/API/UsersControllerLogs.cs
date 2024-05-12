namespace FirstBackend.Core.Constants.Logs.API;

public static class UsersControllerLogs
{
    public const string GetAllUsers = "Получаем всех пользователей";
    public const string GetUserById = "Получаем пользователя по ID {id}";
    public const string GetDevicesByUserId = "Получаем устройства по ID пользователя {userId}";
    public const string GetOrdersByUserId = "Получаем заказы по ID пользователя {userId}";
    public const string CreateUser = "Создаём пользователя с логином {request.Name}, почтой {request.Mail}";
    public const string Login = "Авторизация пользователя";
    public const string UpdateUserData = "Обновляем данные пользователя с ID {id}";
    public const string DeleteUserById = "Удаляем пользователя с ID {id}";
    public const string UpdateUserPassword = "Обновляем пароль пользователя с ID {id}";
    public const string UpdateUserMail = "Обновляем email пользователя с ID {id}";
    public const string UpdateUserRole = "Обновляем роль пользователя с ID {id}";
}
