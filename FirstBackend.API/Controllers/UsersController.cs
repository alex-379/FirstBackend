using FirstBackend.Business.Interfaces;
using FirstBackend.Business.Models.Devices.Responses;
using FirstBackend.Business.Models.Orders.Responses;
using FirstBackend.Business.Models.Users.Requests;
using FirstBackend.Business.Models.Users.Responses;
using FirstBackend.Core.Constants.Logs.API;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace FirstBackend.API.Controllers;

[Authorize]
[ApiController]
[Route("/api/users")]
public class UsersController(IUsersService usersService, IDevicesService devicesService, IOrdersService ordersService) : Controller
{
    private readonly IUsersService _usersService = usersService;
    private readonly IDevicesService _devicesService = devicesService;
    private readonly IOrdersService _ordersService = ordersService;
    private readonly Serilog.ILogger _logger = Log.ForContext<UsersController>();

    [Authorize(Roles = "Administrator")]
    [HttpGet]
    public ActionResult<List<UserResponse>> GetAllUsers()
    {
        _logger.Information(UsersControllerLogs.GetAllUsers);

        return Ok(_usersService.GetAllUsers());
    }

    [HttpGet("{id}")]
    public ActionResult<UserFullResponse> GetUserById(Guid id)
    {
        _usersService.CheckUserRights(id, HttpContext);
        _logger.Information(UsersControllerLogs.GetUserById, id);

        return Ok(_usersService.GetUserById(id));
    }

    [HttpGet("{userId}/devices")]
    public ActionResult<List<DeviceResponse>> GetDevicesByUserId(Guid userId)
    {
        _usersService.CheckUserRights(userId, HttpContext);
        _logger.Information(UsersControllerLogs.GetDevicesByUserId, userId);

        return Ok(_devicesService.GetDevicesByUserId(userId));
    }

    [HttpGet("{userId}/orders")]
    public ActionResult<List<OrderResponse>> GetOrdersByUserId(Guid userId)
    {
        _usersService.CheckUserRights(userId, HttpContext);
        _logger.Information(UsersControllerLogs.GetOrdersByUserId, userId);

        return Ok(_ordersService.GetOrdersByUserId(userId));
    }

    [AllowAnonymous]
    [HttpPost]
    public ActionResult<Guid> CreateUser([FromBody] CreateUserRequest request)
    {
        _logger.Information(UsersControllerLogs.CreateUser, request.Name, request.Mail);
        var id = _usersService.AddUser(request);

        return Ok(id);
    }

    [AllowAnonymous]
    [HttpPost("login")]
    public ActionResult<AuthenticatedResponse> Login([FromBody] LoginUserRequest request)
    {
        _logger.Information(UsersControllerLogs.Login);
        var authenticatedResponse = _usersService.LoginUser(request);

        return Ok(authenticatedResponse);
    }

    [HttpPut("{id}")]
    public ActionResult UpdateUserData([FromRoute] Guid id, [FromBody] UpdateUserDataRequest request)
    {
        _usersService.CheckUserRights(id, HttpContext);
        _logger.Information(UsersControllerLogs.UpdateUserData, id);
        _usersService.UpdateUser(id, request);

        return NoContent();
    }

    [HttpDelete("{id}")]
    public ActionResult DeleteUserById(Guid id)
    {
        _usersService.CheckUserRights(id, HttpContext);
        _logger.Information(UsersControllerLogs.DeleteUserById, id);
        _usersService.DeleteUserById(id);

        return NoContent();
    }

    [HttpPatch("{id}/password")]
    public ActionResult UpdateUserPassword([FromRoute] Guid id, [FromBody] UpdateUserPasswordRequest request)
    {
        _usersService.CheckUserRights(id, HttpContext);
        _logger.Information(UsersControllerLogs.UpdateUserPassword, id);
        _usersService.UpdateUserPassword(id, request);

        return NoContent();
    }

    [HttpPatch("{id}/mail")]
    public ActionResult UpdateUserMail([FromRoute] Guid id, [FromBody] UpdateUserMailRequest request)
    {
        _usersService.CheckUserRights(id, HttpContext);
        _logger.Information(UsersControllerLogs.UpdateUserMail, id);
        _usersService.UpdateUserMail(id, request);

        return NoContent();
    }

    [Authorize(Roles = "Administrator")]
    [HttpPatch("{id}/role")]
    public ActionResult UpdateUserRole([FromRoute] Guid id, [FromBody] UpdateUserRoleRequest request)
    {
        _logger.Information(UsersControllerLogs.UpdateUserRole, id);
        _usersService.UpdateUserRole(id, request);

        return NoContent();
    }
}
