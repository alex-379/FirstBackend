using Microsoft.AspNetCore.Mvc;

namespace FirstBackend.Controllers;

[ApiController]
[Route("[controller]")]
public class UsersController : Controller
{
    private const string _author = "Lx";
    public UsersController()
    {
        
    }

    [HttpGet("GetUser")]
    public string GetAuthor()
    {
        return _author;
    }
}
