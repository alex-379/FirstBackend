using FirstBackend.Core.Dtos;

namespace FirstBackend.DataLayer.Interfaces;

public interface IDevicesRepository
{
    Guid AddDevice(DeviceDto device);
    IEnumerable<DeviceDto> GetDevices();
    DeviceDto GetDeviceById(Guid id);
    IEnumerable<DeviceDto> GetDevicesByUserId(Guid userId);
    void UpdateDevice(DeviceDto device);
    IEnumerable<OrderDto> GetOrdersByDeviceId(Guid deviceId);
}