using FirstBackend.Core.Dtos;

namespace FirstBackend.DataLayer.Interfaces;

public interface IOrdersRepository
{
    Guid AddOrder(OrderDto order);
    IEnumerable<OrderDto> GetAllOrders();
    OrderDto GetOrderById(Guid id);
    IEnumerable<OrderDto> GetOrdersByUserId(Guid userId);
    IEnumerable<OrderDto> GetOrdersByDeviceId(Guid deviceId);
    void UpdateOrder(OrderDto order);
}