using FirstBackend.Core.Dtos;

namespace FirstBackend.Buiseness.Interfaces;

public interface IDevicesService
{
    DeviceDto GetDeviceById(Guid id);
    List<DeviceDto> GetDevicesByUserId(Guid userId);
}