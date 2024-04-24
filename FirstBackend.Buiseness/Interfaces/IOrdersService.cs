using FirstBackend.Core.Dtos;

namespace FirstBackend.Buiseness.Interfaces;

public interface IOrdersService
{
    List<OrderDto> GetAllOrders();
    OrderDto GetOrderById(Guid id);
    OrderDto GetOrderByUserId(Guid userId);
}