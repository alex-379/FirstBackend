using FirstBackend.Buiseness.Interfaces;
using FirstBackend.Core.Dtos;
using FirstBackend.DataLayer.Interfaces;

namespace FirstBackend.Buiseness.Services;

public class OrdersService : IOrdersService
{
    private readonly IOrdersRepository _ordersRepository;

    public OrdersService(IOrdersRepository ordersRepository)
    {
        _ordersRepository = ordersRepository;
    }

    public List<OrderDto> GetAllOrders() => _ordersRepository.GetAllOrders();

    public OrderDto GetOrderById(Guid id) => _ordersRepository.GetOrderById(id);
}
