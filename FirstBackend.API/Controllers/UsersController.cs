using FirstBackend.Buiseness.Interfaces;
using FirstBackend.Core.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace FirstBackend.API.Controllers;

[ApiController]
[Route("/api/users")]
public class UsersController : Controller
{
    private readonly IUsersService _usersService;
    private readonly IDevicesService _devicesService;

    public UsersController(IUsersService usersService, IDevicesService devicesService)
    {
        _usersService = usersService;
        _devicesService = devicesService;
    }

    [HttpGet]
    public List<UserDto> GetAllUsers() => _usersService.GetAllUsers();

    [HttpGet("{id}")]
    public UserDto GetUserById(Guid id) => _usersService.GetUserById(Guid.NewGuid());

    [HttpGet("{userId}/devices")]
    public DeviceDto GetDeviceByUserId(Guid id) => _devicesService.GetDeviceByUserId(Guid.NewGuid());

    [HttpPost]
    public Guid CreateUser(object request) => Guid.NewGuid();

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
