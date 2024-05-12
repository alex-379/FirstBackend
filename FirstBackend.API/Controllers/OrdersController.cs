using FirstBackend.Business.Interfaces;
using FirstBackend.Business.Models.Orders.Requests;
using FirstBackend.Business.Models.Orders.Responses;
using FirstBackend.Core.Constants.Logs.API;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace FirstBackend.API.Controllers;

[Authorize]
[ApiController]
[Route("/api/orders")]
public class OrdersController(IOrdersService ordersService, IUsersService usersService) : Controller
{
    private readonly IOrdersService _ordersService = ordersService;
    private readonly IUsersService _usersService = usersService;
    private readonly Serilog.ILogger _logger = Log.ForContext<OrdersController>();

    [Authorize(Roles = "Administrator")]
    [HttpGet]
    public ActionResult<List<OrderResponse>> GetAllOrders()
    {
        _logger.Information(OrdersControllerLogs.GetAllOrders);

        return Ok(_ordersService.GetAllOrders());
    }

    [Authorize(Roles = "Administrator")]
    [HttpGet("{id}")]
    public ActionResult<OrderFullResponse> GetOrderById(Guid id)
    {
        _logger.Information(OrdersControllerLogs.GetOrderById, id);

        return Ok(_ordersService.GetOrderById(id));
    }

    [HttpPost]
    public ActionResult<Guid> CreateOrder([FromBody] CreateOrderRequest request)
    {
        _logger.Information(OrdersControllerLogs.CreateOrder);
        var id = _ordersService.AddOrder(request);

        return Ok(id);
    }

    [HttpDelete("{id}")]
    public ActionResult DeleteOrderById(Guid id)
    {
        var userId = _usersService.GetUserIdByOrderId(id);
        _usersService.CheckUserRights(userId, HttpContext);
        _logger.Information(OrdersControllerLogs.DeleteOrderById, id);
        _ordersService.DeleteOrderById(id);

        return NoContent();
    }
}
