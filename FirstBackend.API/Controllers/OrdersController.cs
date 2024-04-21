using FirstBackend.Buiseness.Interfaces;
using FirstBackend.Core.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace FirstBackend.API.Controllers;

[ApiController]
[Route("/api/orders")]
public class OrdersController : Controller
{
    private readonly IOrdersService _orderService;

    public OrdersController(IOrdersService ordersService)
    {
        _orderService = ordersService;
    }

    [HttpGet]
    public List<OrderDto> GetAllOrders() => _orderService.GetAllOrders();

    [HttpGet("{id}")]
    public OrderDto GetOrderById() => _orderService.GetOrderById(Guid.NewGuid());
}
