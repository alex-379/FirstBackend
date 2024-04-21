using FirstBackend.Core.Dtos;

namespace FirstBackend.DataLayer.Interfaces;

public interface IDevicesRepository
{
    DeviceDto GetDeviceById(Guid id);
    DeviceDto GetDeviceByUserId(Guid userId);
}