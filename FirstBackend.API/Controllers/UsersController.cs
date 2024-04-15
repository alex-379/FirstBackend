using FirstBackend.Buiseness;
using FirstBackend.Core.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace FirstBackend.Controllers;

[ApiController]
[Route("[controller]")]
public class UsersController : Controller
{
    private const string _author = "Lx";
    private readonly UsersService _userService;

    public UsersController()
    {
        _userService = new ();
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

    [HttpGet("user")]
    public UserDto GetUserById()
    {
        return _userService.GetUserById(Guid.NewGuid());
    }
}
