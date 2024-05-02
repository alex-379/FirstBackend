using FirstBackend.Core.Dtos;
using FirstBackend.DataLayer.Contexts;
using FirstBackend.DataLayer.Interfaces;
using Serilog;

namespace FirstBackend.DataLayer.Repositories;

public class OrdersRepository(MainerLxContext context) : BaseRepository(context), IOrdersRepository
{
    private readonly ILogger _logger = Log.ForContext<OrdersRepository>();

    public Guid AddOrder(OrderDto order)
    {
        _ctx.Orders.Add(order);
        _ctx.SaveChanges();
        _logger.Information("Вносим в базу данных заказ с ID {id}", order.Id);

        return order.Id;
    }

    public List<OrderDto> GetAllOrders()
    {
        _logger.Information("Идём в базу данных и ищем все заказы");

        return [.._ctx.Orders] ;
    }

    public OrderDto GetOrderById(Guid id)
    {
        _logger.Information("Идём в базу данных и ищем заказ по ID {id}", id);

        return _ctx.Orders.FirstOrDefault(o => o.Id == id);
    }

    public List<OrderDto> GetOrdersByUserId(Guid userId)
    {
        _logger.Information("Идём в базу данных и ищем заказы по ID пользователя {userId}", userId);

        return [.. _ctx.Orders.Where(o => o.Customer.Id == userId)];
    }

    public void DeleteOrder(OrderDto order)
    {
        _logger.Information("Идём в базу данных и удаляем заказ с ID {id}", order.Id);
        _ctx.Orders.Remove(order);
        _ctx.SaveChanges();
    }
}
