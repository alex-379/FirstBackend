using FirstBackend.Core.Dtos;
using FirstBackend.DataLayer.Repositories;

namespace FirstBackend.Buiseness
{
    public class OrdersService
    {
        private readonly OrdersRepository _ordersRepository;

        public OrdersService()
        {
            _ordersRepository = new();
        }

        public List<OrderDto> GetAllOrders()
        {
            return _ordersRepository.GetAllOrders();
        }

        public OrderDto GetOrderById(Guid id)
        {
            return _ordersRepository.GetOrderById(id);
        }
    }
}
