using FirstBackend.Buiseness;
using FirstBackend.Core.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace FirstBackend.Controllers;

[ApiController]
[Route("[controller]")]
public class OrdersController:Controller
{
    private readonly OrdersService _orderService;

    public OrdersController()
    {
        _orderService = new ();
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
