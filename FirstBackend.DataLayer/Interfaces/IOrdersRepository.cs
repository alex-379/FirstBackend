using FirstBackend.Core.Dtos;

namespace FirstBackend.DataLayer.Interfaces;

public interface IOrdersRepository
{
    List<OrderDto> GetAllOrders();
    OrderDto GetOrderById(Guid id);
}