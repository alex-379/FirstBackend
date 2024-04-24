using FirstBackend.Buiseness.Interfaces;
using FirstBackend.Core.Dtos;
using FirstBackend.Core.Exeptions;
using FirstBackend.DataLayer.Interfaces;
using FirstBackend.DataLayer.Repositories;
using Serilog;

namespace FirstBackend.Buiseness.Services;

public class OrdersService : IOrdersService
{
    private readonly IOrdersRepository _ordersRepository;
    private readonly ILogger _logger = Log.ForContext<OrdersService>();

    public OrdersService(IOrdersRepository ordersRepository)
    {
        _ordersRepository = ordersRepository;
    }

    public Guid AddOrder(OrderDto order)
    {
        _ordersRepository.AddOrder(order);
        _logger.Information($"Обращаемся к методу репозитория Создание нового заказа с ID {order.Id}");

        return order.Id;
    }

    public List<OrderDto> GetAllOrders()
    {
        _logger.Information($"Обращаемся к методу репозитория Получение всех заказов");

        return _ordersRepository.GetAllOrders();
    }

    public OrderDto GetOrderById(Guid id)
    {
        _logger.Information($"Обращаемся к методу репозитория Получение заказа по ID {id}");
        var order = _ordersRepository.GetOrderById(id) ?? throw new NotFoundException($"Заказ с ID {id} не найден");

        return order;
    }

    public List<OrderDto> GetOrdersByUserId(Guid userId)
    {
        _logger.Information($"Обращаемся к методу репозитория Получение заказа по ID пользователя {userId}");
        var orders = _ordersRepository.GetOrdersByUserId(userId) ?? throw new NotFoundException($"Заказы пользователя с ID {userId} не найдены");

        return orders;
    }
}
