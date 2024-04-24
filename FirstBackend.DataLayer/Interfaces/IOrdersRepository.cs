using FirstBackend.Core.Dtos;

namespace FirstBackend.DataLayer.Interfaces;

public interface IOrdersRepository
{
    Guid AddOrder(OrderDto order);
    List<OrderDto> GetAllOrders();
    OrderDto GetOrderById(Guid id);
    OrderDto GetOrderByUserId(Guid userId);
    void DeleteOrder(OrderDto order);
}