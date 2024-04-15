using FirstBackend.Buiseness.Interfaces;
using FirstBackend.Buiseness.Services;
using FirstBackend.Core.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace FirstBackend.Controllers;

[ApiController]
[Route("[controller]")]
public class OrdersController:Controller
{
    private readonly IOrdersService _orderService;

    public OrdersController(IOrdersService ordersService)
    {
        _orderService = ordersService;
    }

    [HttpGet("data")]
    public int[] GetData()
    {
        return [10, 20];
    }

    [HttpGet]
    public List<OrderDto> GetAllOrders()
    {
        return _orderService.GetAllOrders();
    }

    [HttpGet("user")]
    public OrderDto GetUserById()
    {
        return _orderService.GetOrderById(Guid.NewGuid());
    }
}
