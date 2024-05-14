using AutoMapper;
using FirstBackend.Business.Interfaces;
using FirstBackend.Business.Models.Devices.Requests;
using FirstBackend.Business.Models.Devices.Responses;
using FirstBackend.Core.Constants.Exceptions.Business;
using FirstBackend.Core.Constants.Logs.Business;
using FirstBackend.Core.Dtos;
using FirstBackend.Core.Exсeptions;
using FirstBackend.DataLayer.Interfaces;
using Serilog;

namespace FirstBackend.Business.Services;

public class DevicesService(IDevicesRepository devicesRepository, IOrdersRepository ordersRepository, IMapper mapper) : IDevicesService
{
    private readonly IDevicesRepository _devicesRepository = devicesRepository;
    private readonly IOrdersRepository _ordersRepository = ordersRepository;
    private readonly IMapper _mapper = mapper;
    private readonly ILogger _logger = Log.ForContext<DevicesService>();

    public Guid AddDevice(CreateDeviceRequest request)
    {
        var device = _mapper.Map<DeviceDto>(request);
        device.Id = _devicesRepository.AddDevice(device);
        _logger.Information(DevicesServiceLogs.AddDevice, device.Id);

        return device.Id;
    }

    public List<DeviceResponse> GetDevices()
    {
        _logger.Information(DevicesServiceLogs.GetDevices);
        var devices = _mapper.Map<List<DeviceResponse>>(_devicesRepository.GetDevices());

        return devices;
    }

    public DeviceFullResponse GetDeviceById(Guid id)
    {
        _logger.Information(DevicesServiceLogs.GetDeviceById, id);
        var device = _devicesRepository.GetDeviceById(id) ?? throw new NotFoundException(string.Format(DevicesServiceExceptions.NotFoundException, id));
        var devicerResponse = _mapper.Map<DeviceFullResponse>(device);
        devicerResponse.NumberOrders = _devicesRepository.GetOrdersByDeviceId(id).ToList().Count;

        return devicerResponse;
    }

    public List<DeviceResponse> GetDevicesByUserId(Guid userId)
    {
        _logger.Information(DevicesServiceLogs.GetDevicesByUserId, userId);
        var devices = _devicesRepository.GetDevicesByUserId(userId);
        var devicesResponse = _mapper.Map<List<DeviceResponse>>(devices);

        return devicesResponse;
    }

    public void DeleteDeviceById(Guid id)
    {
        _logger.Information(DevicesServiceLogs.CheckDeviceById, id);
        var device = _devicesRepository.GetDeviceById(id) ?? throw new NotFoundException(string.Format(DevicesServiceExceptions.NotFoundException, id));
        _logger.Information(DevicesServiceLogs.SetIsDeletedDeviceById, id);
        device.IsDeleted = true;
        _logger.Information(DevicesServiceLogs.UpdateDeviceById, id);
        _devicesRepository.UpdateDevice(device);
    }
}

