using FirstBackend.Buiseness.Interfaces;
using FirstBackend.Core.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace FirstBackend.API.Controllers;

[ApiController]
[Route("/api/devices")]
public class DevicesController(IDevicesService deviceService) : Controller
{
    private readonly IDevicesService _deviceService = deviceService;
    private readonly Serilog.ILogger _logger = Log.ForContext<DevicesController>();

    [HttpGet("{id}")]
    public ActionResult<DeviceDto> GetDeviceById(Guid id)
    {
        _logger.Information($"Получаем устройство по ID {id}");

        return Ok(_deviceService.GetDeviceById(id));
    }
}
