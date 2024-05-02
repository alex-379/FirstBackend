using FirstBackend.Buiseness.Interfaces;
using FirstBackend.Buiseness.Models.Devices.Requests;
using FirstBackend.Buiseness.Models.Devices.Responses;
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

    [Authorize(Roles = "Administrator")]
    [HttpGet]
    public ActionResult<List<DeviceResponse>> GetAllDevices()
    {
        _logger.Information($"Получаем всех пользователей");

        return Ok(_deviceService.GetAllDevices());
    }

    [HttpGet("{id}")]
    public ActionResult<DeviceFullResponse> GetDeviceById(Guid id)
    {
        _logger.Information($"Получаем устройство по ID {id}");

        return Ok(_deviceService.GetDeviceById(id));
    }

    [HttpPost]
    public ActionResult<Guid> CreateDevice([FromBody] CreateDeviceRequest request)
    {
        _logger.Information("Создаём устройство");
        var id = _deviceService.AddDevice(request);

        return Ok(id);
    }
}
