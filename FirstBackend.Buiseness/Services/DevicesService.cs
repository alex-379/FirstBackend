using AutoMapper;
using FirstBackend.Buiseness.Interfaces;
using FirstBackend.Buiseness.Models.Devices.Requests;
using FirstBackend.Buiseness.Models.Devices.Responses;
using FirstBackend.Core.Dtos;
using FirstBackend.Core.Exсeptions;
using FirstBackend.DataLayer.Interfaces;
using Serilog;

namespace FirstBackend.Buiseness.Services;

public class DevicesService(IDevicesRepository devicesRepository, IOrdersRepository ordersRepository, IMapper mapper) : IDevicesService
{
    private readonly IDevicesRepository _devicesRepository = devicesRepository;
    private readonly IOrdersRepository _ordersRepository = ordersRepository;
    private readonly IMapper _mapper = mapper;
    private readonly ILogger _logger = Log.ForContext<OrdersService>();

    public Guid AddDevice(CreateDeviceRequest request)
    {
        var device = _mapper.Map<DeviceDto>(request);
        _devicesRepository.AddDevice(device);
        _logger.Information($"Обращаемся к методу репозитория Создание нового устройства с ID {device.Id}");

        return device.Id;
    }

    public List<DeviceResponse> GetAllDevices()
    {
        _logger.Information($"Обращаемся к методу репозитория Получение всех пользователей");
        var devices = _mapper.Map<List<DeviceResponse>>(_devicesRepository.GetAllDevices());

        return devices;
    }

    public DeviceFullResponse GetDeviceById(Guid id)
    {
        _logger.Information($"Обращаемся к методу репозитория Получение устройства по ID {id}");
        var device = _devicesRepository.GetDeviceById(id) ?? throw new NotFoundException($"Устройство с ID {id} не найдено");
        var devicerResponse = _mapper.Map<DeviceFullResponse>(device);
        devicerResponse.NumberOrders = _ordersRepository.GetOrdersByDeviceId(id).Count;

        return devicerResponse;
    }

    public List<DeviceResponse> GetDevicesByUserId(Guid userId)
    {
        _logger.Information($"Обращаемся к методу репозитория Получение устройства по ID пользователя {userId}");
        var devices = _devicesRepository.GetDevicesByUserId(userId) ?? throw new NotFoundException($"Устройства пользователя с ID {userId} не найдены");
        var devicesResponse = _mapper.Map<List<DeviceResponse>>(devices);

        return devicesResponse;
    }

    public void DeleteDeviceById(Guid id)
    {
        _logger.Information($"Проверяем существует ли устройство с ID {id}");
        var device = _devicesRepository.GetDeviceById(id) ?? throw new NotFoundException($"Устройство с ID {id} не найдено");
        _logger.Information($"Обращаемся к методу репозитория Удаление устройства c ID {id}");
        _devicesRepository.DeleteDevice(device);
    }
}

