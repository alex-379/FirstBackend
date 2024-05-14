using FirstBackend.Core.Dtos;

namespace FirstBackend.DataLayer.Interfaces;

public interface IOrdersRepository
{
    Guid AddOrder(OrderDto order);
    IEnumerable<OrderDto> GetOrders();
    OrderDto GetOrderById(Guid id);
    IEnumerable<OrderDto> GetOrdersByUserId(Guid userId);
    void UpdateOrder(OrderDto order);
}