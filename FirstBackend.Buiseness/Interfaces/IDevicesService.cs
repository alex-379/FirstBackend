using FirstBackend.Core.Dtos;

namespace FirstBackend.Buiseness.Interfaces;

public interface IDevicesService
{
    DeviceDto GetDeviceById(Guid id);
    DeviceDto GetDeviceByUserId(Guid userId);
}