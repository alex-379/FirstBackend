using FirstBackend.API.Models.Requests;
using FirstBackend.API.Models.Responses;
using FirstBackend.Buiseness.Interfaces;
using FirstBackend.Core.Dtos;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace FirstBackend.API.Controllers;

[ApiController]
[Route("/api/users")]
public class UsersController : Controller
{
    private readonly IUsersService _usersService;
    private readonly IDevicesService _devicesService;
    private readonly IOrdersService _ordersService;
    private readonly Serilog.ILogger _logger = Log.ForContext<UsersController>();

    public UsersController(IUsersService usersService, IDevicesService devicesService, IOrdersService ordersService)
    {
        _usersService = usersService;
        _devicesService = devicesService;
        _ordersService = ordersService;
    }

    [HttpGet]
    public ActionResult<List<UserResponse>> GetAllUsers()
    {
        _usersService.GetAllUsers();
        return Ok(new List<UserResponse>());
    }


    [HttpGet("{id}")]
    public ActionResult<UserFullResponse> GetUserById(Guid id)
    {
        _logger.Information($"Получаем пользователя по ID {id}");
        _usersService.GetUserById(Guid.NewGuid());
        return Ok(new UserFullResponse());
    }

    [HttpGet("{userId}/devices")]
    public DeviceDto GetDeviceByUserId(Guid userId) => _devicesService.GetDeviceByUserId(Guid.NewGuid());

    [HttpGet("{userId}/orders")]
    public OrderDto GetOrderByUserId(Guid userId) => _ordersService.GetOrderByUserId(Guid.NewGuid());

    [HttpPost]
    public ActionResult<Guid> CreateUser([FromBody] CreateUserRequest request)
    {
        _logger.Information($"{request.UserName} {request.Mail}");
        var id = _usersService.AddUser(new()
        {
            UserName = request.UserName,
            Mail = request.Mail,
            Password = request.Password
        });

        return Ok(id);
    }

    [HttpPut("{id}")]
    public ActionResult UpdateUser([FromRoute]Guid id, [FromBody]object request)
    {
        return NoContent();
    } 

    [HttpDelete("{id}")]
    public ActionResult DeleteUserById(Guid id)
    {
        try
        {
            _usersService.DeleteUserById(id);
        }
        catch (Exception ex)
        {
            return NotFound(ex.Message);
        }

        return NoContent();
    }
}
