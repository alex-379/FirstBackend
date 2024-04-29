using FirstBackend.Buiseness.Interfaces;
using FirstBackend.Core.Dtos;
using FirstBackend.Core.Exeptions;
using FirstBackend.DataLayer.Interfaces;
using Serilog;

namespace FirstBackend.Buiseness.Services;

public class DevicesService(IDevicesRepository devicesRepository) : IDevicesService
{
    private readonly IDevicesRepository _devicesRepository = devicesRepository;
    private readonly ILogger _logger = Log.ForContext<OrdersService>();

    public Guid AddDevice(DeviceDto device)
    {
        _devicesRepository.AddDevice(device);
        _logger.Information($"Обращаемся к методу репозитория Создание нового устройства с ID {device.Id}");

        return device.Id;
    }

    public DeviceDto GetDeviceById(Guid id)
    {
        _logger.Information($"Обращаемся к методу репозитория Получение устройства по ID {id}");
        var device = _devicesRepository.GetDeviceById(id) ?? throw new NotFoundException($"Устройство с ID {id} не найдено");

        return device;
    }

    public List<DeviceDto> GetDevicesByUserId(Guid userId)
    {
        _logger.Information($"Обращаемся к методу репозитория Получение устройства по ID пользователя {userId}");
        var devices = _devicesRepository.GetDevicesByUserId(userId) ?? throw new NotFoundException($"Устройства пользователя с ID {userId} не найдены");

        _devicesRepository.GetDevicesByUserId(userId);

        return devices;
    }
}
