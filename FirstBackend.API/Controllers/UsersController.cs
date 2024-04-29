using FirstBackend.API.Configuration;
using FirstBackend.API.Models.Requests;
using FirstBackend.API.Models.Responses;
using FirstBackend.Buiseness.Interfaces;
using FirstBackend.Core.Dtos;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace FirstBackend.API.Controllers;

[ApiController]
[Route("/api/users")]
public class UsersController(IUsersService usersService, IDevicesService devicesService, IOrdersService ordersService, EnviromentVariables enviromentVariables) : Controller
{
    private readonly IUsersService _usersService = usersService;
    private readonly IDevicesService _devicesService = devicesService;
    private readonly IOrdersService _ordersService = ordersService;
    private readonly EnviromentVariables _enviromentVariables = enviromentVariables;
    private readonly Serilog.ILogger _logger = Log.ForContext<UsersController>();

    [HttpGet]
    public ActionResult<List<UserResponse>> GetAllUsers()
    {
        _logger.Information($"Получаем всех пользователей");

        return Ok(_usersService.GetAllUsers());
    }

    [HttpGet("{id}")]
    public ActionResult<UserFullResponse> GetUserById(Guid id)
    {
        _logger.Information($"Получаем пользователя по ID {id}");

        return Ok(_usersService.GetUserById(id));
    }

    [HttpGet("{userId}/devices")]
    public ActionResult<List<DeviceDto>> GetDevicesByUserId(Guid userId)
    {
        _logger.Information($"Получаем устройства по ID пользователя {userId}");

        return Ok(_devicesService.GetDevicesByUserId(userId));
    }

    [HttpGet("{userId}/orders")]
    public ActionResult<List<OrderDto>> GetOrdersByUserId(Guid userId)
    {
        _logger.Information($"Получаем заказы по ID пользователя {userId}");

        return Ok(_ordersService.GetOrdersByUserId(userId));
    }

    [HttpPost]
    public ActionResult<Guid> CreateUser([FromBody] CreateUserRequest request)
    {
        var secret = _enviromentVariables.SecretPassword;
        _logger.Information($"Создаём пользователя с логином {request.UserName}, почтой {request.Mail}");
        var id = _usersService.AddUser(secret, new()
        {
            UserName = request.UserName,
            Mail = request.Mail,
            Password = request.Password
        });

        return Ok(id);
    }

    [HttpPut("{id}")]
    public ActionResult UpdateUserData([FromRoute] Guid id, [FromBody] UpdateUserDataRequest request)
    {
        _logger.Information($"Обновляем данные пользователя с ID {id}");
        _usersService.UpdateUser(id, new()
        {
            UserName = request.UserName,
            Mail = request.Mail,
        });

        return NoContent();
    }

    [HttpDelete("{id}")]
    public ActionResult DeleteUserById(Guid id)
    {
        _usersService.DeleteUserById(id);

        return NoContent();
    }

    [HttpPatch("{id}")]
    public ActionResult UpdateUserPassword([FromRoute] Guid id, [FromBody] UpdateUserPasswordRequest request)
    {
        _logger.Information($"Обновляем пароль пользователя с ID {id}");
        _usersService.UpdateUser(id, new()
        {
            Password = request.Password,
        });

        return NoContent();
    }
}
