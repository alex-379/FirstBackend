using FirstBackend.Business.Models.Orders.Requests;
using FirstBackend.Business.Models.Orders.Responses;

namespace FirstBackend.Business.Interfaces;

public interface IOrdersService
{
    List<OrderResponse> GetOrders();
    OrderFullResponse GetOrderById(Guid id);
    List<OrdersByUserResponse> GetOrdersByUserId(Guid userId);
    Guid AddOrder(CreateOrderRequest request, Guid customerId);
    void DeleteOrderById(Guid id);
}