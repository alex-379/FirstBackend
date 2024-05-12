using FirstBackend.Core.Constants.Logs.DataLayer;
using FirstBackend.Core.Dtos;
using FirstBackend.DataLayer.Contexts;
using FirstBackend.DataLayer.Interfaces;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace FirstBackend.DataLayer.Repositories;

public class OrdersRepository(MainerLxContext context) : BaseRepository<MainerLxContext>(context), IOrdersRepository
{
    private readonly ILogger _logger = Log.ForContext<OrdersRepository>();

    public Guid AddOrder(OrderDto order)
    {
        _ctx.Orders.Add(order);
        _ctx.SaveChanges();
        _logger.Information(OrdersRepositoryLogs.AddOrder, order.Id);

        return order.Id;
    }

    public IEnumerable<OrderDto> GetAllOrders()
    {
        _logger.Information(OrdersRepositoryLogs.GetAllOrders);

        return _ctx.Orders
            .Where(o => !o.IsDeleted);
    }

    public OrderDto GetOrderById(Guid id)
    {
        _logger.Information(OrdersRepositoryLogs.GetOrderById, id);

        return _ctx.Orders
            .Include(o => o.Customer)
            .Include(o => o.Devices)
            .FirstOrDefault(o => o.Id == id
            && !o.IsDeleted);
    }

    public IEnumerable<OrderDto> GetOrdersByUserId(Guid userId)
    {
        _logger.Information(OrdersRepositoryLogs.GetOrdersByUserId, userId);

        return _ctx.Orders
            .Where(o => o.Customer.Id == userId
                && !o.IsDeleted);
    }

    public IEnumerable<OrderDto> GetOrdersByDeviceId(Guid deviceId)
    {
        _logger.Information(OrdersRepositoryLogs.GetOrdersByDeviceId, deviceId);

        return [.. _ctx.Orders
            .Where(o => !o.IsDeleted
                && o.Devices
                .Any(d => d.Id == deviceId)
            )];
    }

    public void UpdateOrder(OrderDto order)
    {
        _logger.Information(OrdersRepositoryLogs.UpdateOrder, order.Id);
        _ctx.Orders.Update(order);
        _ctx.SaveChanges();
    }
}
