namespace FirstBackend.Core.Constants.Logs.DataLayer;

public static class OrdersRepositoryLogs
{
    public const string AddOrder = "Вносим в базу данных заказ с ID {id}";
    public const string GetOrders = "Идём в базу данных и ищем все заказы";
    public const string GetOrderById = "Идём в базу данных и ищем заказ по ID {id}";
    public const string GetOrdersByUserId = "Идём в базу данных и ищем заказы по ID пользователя {userId}";
    public const string UpdateOrder = "Идём в базу данных и обновляем заказ с ID {id}";
    public const string GetUserByOrderId = "Идём в базу данных и ищем пользователя по ID заказа {orderId}";
}
