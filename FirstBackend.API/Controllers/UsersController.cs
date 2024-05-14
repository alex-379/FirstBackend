using FirstBackend.API.Configuration.Filters;
using FirstBackend.Business.Interfaces;
using FirstBackend.Business.Models.Devices.Responses;
using FirstBackend.Business.Models.Orders.Responses;
using FirstBackend.Business.Models.Users.Requests;
using FirstBackend.Business.Models.Users.Responses;
using FirstBackend.Core.Constants;
using FirstBackend.Core.Constants.Logs.API;
using FirstBackend.Core.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace FirstBackend.API.Controllers;

[Authorize]
[ApiController]
[Route(ControllersRoutes.UsersController)]
public class UsersController(IUsersService usersService, IDevicesService devicesService, IOrdersService ordersService) : Controller
{
    private readonly IUsersService _usersService = usersService;
    private readonly IDevicesService _devicesService = devicesService;
    private readonly IOrdersService _ordersService = ordersService;
    private readonly Serilog.ILogger _logger = Log.ForContext<UsersController>();

    [Authorize(Roles = nameof(UserRole.Administrator))]
    [HttpGet]
    public ActionResult<List<UserResponse>> GetUsers()
    {
        _logger.Information(UsersControllerLogs.GetUsers);

        return Ok(_usersService.GetUsers());
    }

    [AuthorizationFilterByUserId]
    [HttpGet(ControllersRoutes.Id)]
    public ActionResult<UserFullResponse> GetUserById(Guid id)
    {
        _logger.Information(UsersControllerLogs.GetUserById, id);

        return Ok(_usersService.GetUserById(id));
    }

    [AuthorizationFilterByUserId]
    [HttpGet(ControllersRoutes.DevicesByUserId)]
    public ActionResult<List<DeviceResponse>> GetDevicesByUserId(Guid userId)
    {
        _logger.Information(UsersControllerLogs.GetDevicesByUserId, userId);

        return Ok(_devicesService.GetDevicesByUserId(userId));
    }

    [AuthorizationFilterByUserId]
    [HttpGet(ControllersRoutes.OrdersByUserId)]
    public ActionResult<List<OrderResponse>> GetOrdersByUserId(Guid userId)
    {
        _logger.Information(UsersControllerLogs.GetOrdersByUserId, userId);

        return Ok(_ordersService.GetOrdersByUserId(userId));
    }

    [AllowAnonymous]
    [HttpPost]
    public ActionResult<Guid> CreateUser([FromBody] CreateUserRequest request)
    {
        _logger.Information(UsersControllerLogs.CreateUser, request.Name, request.Mail);
        var id = _usersService.AddUser(request);

        return Created($"{ControllersRoutes.Host}{ControllersRoutes.UsersController}/{id}", id);
    }

    [AllowAnonymous]
    [HttpPost(ControllersRoutes.Login)]
    public ActionResult<AuthenticatedResponse> Login([FromBody] LoginUserRequest request)
    {
        _logger.Information(UsersControllerLogs.Login);
        var authenticatedResponse = _usersService.LoginUser(request);

        return Ok(authenticatedResponse);
    }

    [AuthorizationFilterByUserId]
    [HttpPut(ControllersRoutes.Id)]
    public ActionResult UpdateUserData([FromRoute] Guid id, [FromBody] UpdateUserDataRequest request)
    {
        _logger.Information(UsersControllerLogs.UpdateUserData, id);
        _usersService.UpdateUser(id, request);

        return NoContent();
    }

    [AuthorizationFilterByUserId]
    [HttpDelete(ControllersRoutes.Id)]
    public ActionResult DeleteUserById(Guid id)
    {
        _logger.Information(UsersControllerLogs.DeleteUserById, id);
        _usersService.DeleteUserById(id);

        return NoContent();
    }

    [AuthorizationFilterByUserId]
    [HttpPatch(ControllersRoutes.UserPassword)]
    public ActionResult UpdateUserPassword([FromRoute] Guid id, [FromBody] UpdateUserPasswordRequest request)
    {
        _logger.Information(UsersControllerLogs.UpdateUserPassword, id);
        _usersService.UpdateUserPassword(id, request);

        return NoContent();
    }

    [AuthorizationFilterByUserId]
    [HttpPatch(ControllersRoutes.UserMail)]
    public ActionResult UpdateUserMail([FromRoute] Guid id, [FromBody] UpdateUserMailRequest request)
    {
        _logger.Information(UsersControllerLogs.UpdateUserMail, id);
        _usersService.UpdateUserMail(id, request);

        return NoContent();
    }

    [Authorize(Roles = nameof(UserRole.Administrator))]
    [HttpPatch(ControllersRoutes.UserRole)]
    public ActionResult UpdateUserRole([FromRoute] Guid id, [FromBody] UpdateUserRoleRequest request)
    {
        _logger.Information(UsersControllerLogs.UpdateUserRole, id);
        _usersService.UpdateUserRole(id, request);

        return NoContent();
    }
}
