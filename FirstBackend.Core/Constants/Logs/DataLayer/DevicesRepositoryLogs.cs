namespace FirstBackend.Core.Constants.Logs.DataLayer;

public static class DevicesRepositoryLogs
{
    public const string AddDevice = "Вносим в базу данных устройство с ID {id}";
    public const string GetDevices = "Идём в базу данных и ищем все устройства";
    public const string GetDeviceById = "Идём в базу данных и ищем устройство по ID {id}";
    public const string GetDevicesByUserId = "Идём в базу данных и ищем устройства по ID пользователя {userId}";
    public const string UpdateDevice = "Идём в базу данных и обновляем устройство с ID {id}";
    public const string GetOrdersByDeviceId = "Идём в базу данных и ищем заказы по ID устройства {deviceId}";
}
