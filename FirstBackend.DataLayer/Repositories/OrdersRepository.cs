using FirstBackend.Core.Dtos;
using FirstBackend.DataLayer.Interfaces;

namespace FirstBackend.DataLayer.Repositories
{
    public class OrdersRepository : IOrdersRepository
    {
        public OrdersRepository()
        {

        }

        public List<OrderDto> GetAllOrders()
        {
            return [];
        }

        public OrderDto GetOrderById(Guid id)
        {
            return new()
            {
                Id = id,
                Description = "OrderTest",
            };
        }

    }
}
