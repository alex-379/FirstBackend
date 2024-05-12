using FirstBackend.Core.Dtos;

namespace FirstBackend.DataLayer.Interfaces;

public interface IDevicesRepository
{
    Guid AddDevice(DeviceDto device);
    IEnumerable<DeviceDto> GetAllDevices();
    DeviceDto GetDeviceById(Guid id);
    IEnumerable<DeviceDto> GetDevicesByUserId(Guid userId);
    void UpdateDevice(DeviceDto device);
}