using FirstBackend.Buiseness.Interfaces;
using FirstBackend.Core.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace FirstBackend.API.Controllers;

[ApiController]
[Route("/api/devices")]
public class DevicesController : Controller
{
    private readonly IDevicesService _deviceService;

    public DevicesController(IDevicesService deviceService)
    {
        _deviceService = deviceService;
    }

    [HttpGet("{id}")]
    public ActionResult<DeviceDto> GetDeviceById(Guid id)
    {
        var device = _deviceService.GetDeviceById(id);

        if (device is null)
        {
            return NotFound($"Устройство с ID {id} не найдено");
        }

        return Ok(device);
    }

    //[HttpGet("{id}")]
    //public DeviceDto GetDevice([FromQuery]Guid id, )
    //{
    //    return _deviceService.GetDeviceById(id);
    //}
}
