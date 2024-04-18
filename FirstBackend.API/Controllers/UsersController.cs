using FirstBackend.Buiseness.Interfaces;
using FirstBackend.Core.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace FirstBackend.Controllers;

[ApiController]
[Route("/users")]
public class UsersController : Controller
{
    private const string _author = "Lx";
    private readonly IUsersService _userService;

    public UsersController(IUsersService usersService)
    {
        _userService = usersService;
    }

    [HttpGet("author")]
    public string GetAuthor()
    {
        return _author;
    }

    [HttpGet]
    public List<UserDto> GetAllUsers()
    {
        return _userService.GetAllUsers();
    }

    [HttpGet("{id}")]
    public UserDto GetUserById(Guid id)
    {
        return _userService.GetUserById(Guid.NewGuid());
    }

    [HttpPost]
    public Guid CreateUser(object request)
    {
        return Guid.NewGuid();
    }

    [HttpPut("{id}")]
    public Guid UpdateUser([FromRoute]Guid id, [FromBody]object request)
    {
        return Guid.NewGuid();
    }

    [HttpDelete("{id}")]
    public void DeleteUserById(Guid id)
    {

    }
}
