using FirstBackend.Business.Models.Orders.Requests;
using FirstBackend.Business.Models.Orders.Responses;

namespace FirstBackend.Business.Interfaces;

public interface IOrdersService
{
    List<OrderResponse> GetAllOrders();
    OrderFullResponse GetOrderById(Guid id);
    List<OrdersByUserResponse> GetOrdersByUserId(Guid userId);
    Guid AddOrder(CreateOrderRequest request);
    void DeleteOrderById(Guid id);
}