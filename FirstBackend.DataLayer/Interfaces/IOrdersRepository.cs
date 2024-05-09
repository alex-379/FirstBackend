using FirstBackend.Core.Dtos;

namespace FirstBackend.DataLayer.Interfaces;

public interface IOrdersRepository
{
    Guid AddOrder(OrderDto order);
    List<OrderDto> GetAllOrders();
    OrderDto GetOrderById(Guid id);
    List<OrderDto> GetOrdersByUserId(Guid userId);
    List<OrderDto> GetOrdersByDeviceId(Guid deviceId);
    void UpdateOrder(OrderDto order);
}