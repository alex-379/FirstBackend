using FirstBackend.Core.Dtos;
using FirstBackend.DataLayer.Interfaces;

namespace FirstBackend.DataLayer.Repositories;

public class OrdersRepository : BaseRepository, IOrdersRepository
{
    public OrdersRepository(MainerLxContext context) : base(context)
    {

    }

    public List<OrderDto> GetAllOrders() => [.. _ctx.Orders];

    public OrderDto GetOrderById(Guid id) => _ctx.Orders.FirstOrDefault(o => o.Id == id);
}
