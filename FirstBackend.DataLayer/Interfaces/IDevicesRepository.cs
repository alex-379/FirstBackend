using FirstBackend.Core.Dtos;

namespace FirstBackend.DataLayer.Interfaces;

public interface IDevicesRepository
{
    Guid AddDevice(DeviceDto device);
    DeviceDto GetDeviceById(Guid id);
    List<DeviceDto> GetDevicesByUserId(Guid userId);
    void DeleteDevice(DeviceDto device);
}