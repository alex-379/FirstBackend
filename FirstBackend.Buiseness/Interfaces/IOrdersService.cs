using FirstBackend.Buiseness.Models.Orders.Requests;
using FirstBackend.Buiseness.Models.Orders.Responses;

namespace FirstBackend.Buiseness.Interfaces;

public interface IOrdersService
{
    List<OrderResponse> GetAllOrders();
    OrderFullResponse GetOrderById(Guid id);
    List<OrdersByUserResponse> GetOrdersByUserId(Guid userId);
    Guid AddOrder(CreateOrderRequest request);
    void DeleteOrderById(Guid id);
}