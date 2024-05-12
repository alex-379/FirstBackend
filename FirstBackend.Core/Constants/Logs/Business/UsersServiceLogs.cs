namespace FirstBackend.Core.Constants.Logs.Business;

public static class UsersServiceLogs
{
    public const string SetRoleClient = "Устанавливаем роль Клиент";
    public const string SetLowerRegister = "Переводим почту и имя в нижний регистр";
    public const string BeginTransaction = "Начало транзакции для базы данных {_ctx}]";
    public const string AddUser = "Обращаемся к методу репозитория Создание нового пользователя";
    public const string CompleteUser = "Создан новый пользователь с ID {id}";
    public const string AddSalt = "Обращаемся к методу репозитория Добавление соли для пользователя";
    public const string CommitTransaction = "Подтверждение транзакции для базы данных {_ctx}]";
    public const string CheckUserByMail = "Проверяем есть ли пользователь в базе данных";
    public const string CheckUserById = "Проверяем существует ли пользователь с ID {id}";
    public const string CheckUserPassword = "Проверка аутентификационных данных";
    public const string GetAllUsers = "Обращаемся к методу репозитория Получение всех пользователей";
    public const string GetUserById = "Обращаемся к методу репозитория Получение пользователя по ID {id}";
    public const string UpdateUserData = "Обновляем данные пользователя с ID {id} из запроса";
    public const string UpdateUserPassword = "Обновляем пароль пользователя с ID {id} из запроса";
    public const string UpdateUserMail = "Обновляем почту пользователя с ID {id} из запроса";
    public const string UpdateUserRole = "Обновляем роль пользователя с ID {id} из запроса";
    public const string UpdateUserById = "Обращаемся к методу репозитория Обновление пользователя c ID {id}";
    public const string GetSaltByUserId = "Обращаемся к методу репозитория Получение соли пользователя с ID {id}";
    public const string DeleteSalt = "Обращаемся к методу репозитория Удаление соли пользователя с ID {id}";
    public const string UpdateSalt = "Обращаемся к методу репозитория Обновление соли для пользователя";
    public const string CompleteSalt = "Обновлена соль для пользователя с ID {id}";
    public const string SetIsDeletedUserById = "Устанавливаем IsDeleted=true для пользователя c ID {id}";
    public const string GetUserByOrderId = "Обращаемся к методу репозитория Получение пользователя по ID заказа {orderId}";
}
