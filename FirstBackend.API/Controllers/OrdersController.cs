using FirstBackend.API.Configuration.Filters;
using FirstBackend.Business.Interfaces;
using FirstBackend.Business.Models.Orders.Requests;
using FirstBackend.Business.Models.Orders.Responses;
using FirstBackend.Core.Constants;
using FirstBackend.Core.Constants.Logs.API;
using FirstBackend.Core.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using System.Security.Claims;

namespace FirstBackend.API.Controllers;

[Authorize]
[ApiController]
[Route(ControllersRoutes.OrdersController)]
public class OrdersController(IOrdersService ordersService) : Controller
{
    private readonly IOrdersService _ordersService = ordersService;
    private readonly Serilog.ILogger _logger = Log.ForContext<OrdersController>();

    [Authorize(Roles = nameof(UserRole.Administrator))]
    [HttpGet]
    public ActionResult<List<OrderResponse>> GetOrders()
    {
        _logger.Information(OrdersControllerLogs.GetOrders);

        return Ok(_ordersService.GetOrders());
    }

    [Authorize(Roles = nameof(UserRole.Administrator))]
    [HttpGet(ControllersRoutes.Id)]
    public ActionResult<OrderFullResponse> GetOrderById(Guid id)
    {
        _logger.Information(OrdersControllerLogs.GetOrderById, id);

        return Ok(_ordersService.GetOrderById(id));
    }

    [HttpPost]
    public ActionResult<Guid> CreateOrder([FromBody] CreateOrderRequest request)
    {
        _logger.Information(OrdersControllerLogs.CreateOrder);
        var currentUserId = new Guid(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));
        var id = _ordersService.AddOrder(request, currentUserId);

        return Created($"{ControllersRoutes.Host}{ControllersRoutes.OrdersController}/{id}", id);
    }

    [TypeFilter(typeof(AuthorizationFilterByOrderId))]
    [HttpDelete(ControllersRoutes.Id)]
    public ActionResult DeleteOrderById(Guid id)
    {
        _logger.Information(OrdersControllerLogs.DeleteOrderById, id);
        _ordersService.DeleteOrderById(id);

        return NoContent();
    }
}
