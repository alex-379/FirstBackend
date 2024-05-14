namespace FirstBackend.Core.Constants.Logs.Business;

public static class OrdersServiceLogs
{
    public const string AddOrder = "Обращаемся к методу репозитория Создание нового заказа с ID {id}";
    public const string GetOrders = "Обращаемся к методу репозитория Получение всех заказов";
    public const string GetOrderById = "Обращаемся к методу репозитория Получение заказа по ID {id}";
    public const string GetOrdersByUserId = "Обращаемся к методу репозитория Получение заказа по ID пользователя {userId}";
    public const string CheckOrderById = "Проверяем существует ли заказ с ID {id}";
    public const string SetIsDeletedOrderById = "Устанавливаем IsDeleted=true для заказа c ID {id}";
    public const string UpdateOrderById = "Обращаемся к методу репозитория Обновление заказа c ID {id}";
}
