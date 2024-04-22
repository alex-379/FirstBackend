using FirstBackend.Buiseness.Interfaces;
using FirstBackend.Core.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace FirstBackend.API.Controllers;

[ApiController]
[Route("/api/orders")]
public class OrdersController : Controller
{
    private readonly IOrdersService _ordersService;

    public OrdersController(IOrdersService ordersService)
    {
        _ordersService = ordersService;
    }

    [HttpGet]
    public List<OrderDto> GetAllOrders() => _ordersService.GetAllOrders();

    [HttpGet("{id}")]
    public OrderDto GetOrderById() => _ordersService.GetOrderById(Guid.NewGuid());
}
