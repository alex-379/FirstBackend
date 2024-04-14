using Microsoft.AspNetCore.Mvc;

namespace FirstBackend.Controllers;

[ApiController]
[Route("[controller]")]
public class OrdersController:Controller
{
    public OrdersController()
    {
        
    }

    [HttpGet("GetOrder")]
    public int[] GetData()
    {
        return [1, 2];
    }
}
