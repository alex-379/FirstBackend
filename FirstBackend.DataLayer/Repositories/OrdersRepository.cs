using FirstBackend.Core.Dtos;

namespace FirstBackend.DataLayer.Repositories
{
    public class OrdersRepository
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
