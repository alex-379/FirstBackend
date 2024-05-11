using FirstBackend.Buiseness.Interfaces;
using FirstBackend.Buiseness.Models.Devices.Responses;
using FirstBackend.Buiseness.Models.Orders.Responses;
using FirstBackend.Buiseness.Models.Users.Requests;
using FirstBackend.Buiseness.Models.Users.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace FirstBackend.API.Controllers;

[Authorize]
[ApiController]
[Route("/api/users")]
public class UsersController(IUsersService usersService, IDevicesService devicesService, IOrdersService ordersService, ITokensService tokensService) : Controller
{
    private readonly IUsersService _usersService = usersService;
    private readonly IDevicesService _devicesService = devicesService;
    private readonly IOrdersService _ordersService = ordersService;
    private readonly ITokensService _tokensService = tokensService;
    private readonly Serilog.ILogger _logger = Log.ForContext<UsersController>();

    [Authorize(Roles = "Administrator")]
    [HttpGet]
    public ActionResult<List<UserResponse>> GetAllUsers()
    {
        _logger.Information($"Получаем всех пользователей");

        return Ok(_usersService.GetAllUsers());
    }

    [HttpGet("{id}")]
    public ActionResult<UserFullResponse> GetUserById(Guid id)
    {
        _usersService.CheckUserRights(id, HttpContext);
        _logger.Information($"Получаем пользователя по ID {id}");

        return Ok(_usersService.GetUserById(id));
    }

    [HttpGet("{userId}/devices")]
    public ActionResult<List<DeviceResponse>> GetDevicesByUserId(Guid userId)
    {
        _usersService.CheckUserRights(userId, HttpContext);
        _logger.Information($"Получаем устройства по ID пользователя {userId}");

        return Ok(_devicesService.GetDevicesByUserId(userId));
    }

    [HttpGet("{userId}/orders")]
    public ActionResult<List<OrderResponse>> GetOrdersByUserId(Guid userId)
    {
        _usersService.CheckUserRights(userId, HttpContext);
        _logger.Information($"Получаем заказы по ID пользователя {userId}");

        return Ok(_ordersService.GetOrdersByUserId(userId));
    }

    [AllowAnonymous]
    [HttpPost]
    public ActionResult<Guid> CreateUser([FromBody] CreateUserRequest request)
    {
        _logger.Information($"Создаём пользователя с логином {request.Name}, почтой {request.Mail}");
        var id = _usersService.AddUser(request);

        return Ok(id);
    }

    [AllowAnonymous]
    [HttpPost("login")]
    public ActionResult<AuthenticatedResponse> Login([FromBody] LoginUserRequest request)
    {
        _logger.Information($"Авторизация пользователя");
        var authenticatedResponse = _usersService.LoginUser(request);

        return Ok(authenticatedResponse);
    }

    [HttpPut("{id}")]
    public ActionResult UpdateUserData([FromRoute] Guid id, [FromBody] UpdateUserDataRequest request)
    {
        _usersService.CheckUserRights(id, HttpContext);
        _logger.Information($"Обновляем данные пользователя с ID {id}");
        _usersService.UpdateUser(id, request);

        return NoContent();
    }

    [HttpDelete("{id}")]
    public ActionResult DeleteUserById(Guid id)
    {
        _usersService.CheckUserRights(id, HttpContext);
        _logger.Information($"Удаляем пользователя с ID {id}");
        _usersService.DeleteUserById(id);

        return NoContent();
    }

    [HttpPatch("{id}/password")]
    public ActionResult UpdateUserPassword([FromRoute] Guid id, [FromBody] UpdateUserPasswordRequest request)
    {
        _usersService.CheckUserRights(id, HttpContext);
        _logger.Information($"Обновляем пароль пользователя с ID {id}");
        _usersService.UpdateUserPassword(id, request);

        return NoContent();
    }

    [HttpPatch("{id}/mail")]
    public ActionResult UpdateUserMail([FromRoute] Guid id, [FromBody] UpdateUserMailRequest request)
    {
        _usersService.CheckUserRights(id, HttpContext);
        _logger.Information($"Обновляем email пользователя с ID {id}");
        _usersService.UpdateUserMail(id, request);

        return NoContent();
    }

    [Authorize(Roles = "Administrator")]
    [HttpPatch("{id}/role")]
    public ActionResult UpdateUserRole([FromRoute] Guid id, [FromBody] UpdateUserRoleRequest request)
    {
        _logger.Information($"Обновляем email пользователя с ID {id}");
        _usersService.UpdateUserRole(id, request);

        return NoContent();
    }
}
