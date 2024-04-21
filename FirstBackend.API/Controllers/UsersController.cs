using FirstBackend.Buiseness.Interfaces;
using FirstBackend.Core.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace FirstBackend.API.Controllers;

[ApiController]
[Route("/api/users")]
public class UsersController : Controller
{
    private readonly IUsersService _userService;

    public UsersController(IUsersService usersService)
    {
        _userService = usersService;
    }

    [HttpGet]
    public List<UserDto> GetAllUsers() => _userService.GetAllUsers();

    [HttpGet("{id}")]
    public UserDto GetUserById(Guid id) => _userService.GetUserById(Guid.NewGuid());

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
        return NoContent();
    }
}
