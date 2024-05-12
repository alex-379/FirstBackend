namespace FirstBackend.Core.Constants.Logs.Business;

public static class DevicesServiceLogs
{
    public const string AddDevice = "Обращаемся к методу репозитория Создание нового устройства с ID {id}";
    public const string GetAllDevices = "Обращаемся к методу репозитория Получение всех пользователей";
    public const string GetDeviceById = "Обращаемся к методу репозитория Получение устройства по ID {id}";
    public const string GetDevicesByUserId = "Обращаемся к методу репозитория Получение устройства по ID пользователя {userId}";
    public const string CheckDeviceById = "Проверяем существует ли устройство с ID {id}";
    public const string SetIsDeletedDeviceById = "Устанавливаем IsDeleted=true для устройства c ID {id}";
    public const string UpdateDeviceById = "Обращаемся к методу репозитория Обновление устройства c ID {id}";
}
