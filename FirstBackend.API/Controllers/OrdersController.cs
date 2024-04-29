using FirstBackend.Buiseness.Interfaces;
using FirstBackend.Core.Dtos;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace FirstBackend.API.Controllers;

[ApiController]
[Route("/api/orders")]
public class OrdersController(IOrdersService ordersService) : Controller
{
    private readonly IOrdersService _ordersService = ordersService;
    private readonly Serilog.ILogger _logger = Log.ForContext<OrdersController>();

    [HttpGet]
    public ActionResult<List<OrderDto>> GetAllOrders()
    {
        _logger.Information($"Получаем все заказы");

        return Ok(_ordersService.GetAllOrders());
    }

    [HttpGet("{id}")]
    public ActionResult<OrderDto> GetOrderById(Guid id)
    {
        _logger.Information($"Получаем заказ по ID {id}");

        return Ok(_ordersService.GetOrderById(id));
    }
}
