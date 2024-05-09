using FirstBackend.Buiseness.Interfaces;
using FirstBackend.Buiseness.Models.Devices.Requests;
using FirstBackend.Buiseness.Models.Devices.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace FirstBackend.API.Controllers;

[Authorize(Roles = "Administrator")]
[ApiController]
[Route("/api/devices")]
public class DevicesController(IDevicesService deviceService) : Controller
{
    private readonly IDevicesService _deviceService = deviceService;
    private readonly Serilog.ILogger _logger = Log.ForContext<DevicesController>();

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

    [HttpDelete("{id}")]
    public ActionResult DeleteDeviceById(Guid id)
    {
        _logger.Information($"Удаляем устройство с ID {id}");
        _deviceService.DeleteDeviceById(id);

        return NoContent();
    }
}
