using FirstBackend.Business.Models.Devices.Requests;
using FirstBackend.Business.Models.Devices.Responses;

namespace FirstBackend.Business.Interfaces;

public interface IDevicesService
{
    List<DeviceResponse> GetAllDevices();
    DeviceFullResponse GetDeviceById(Guid id);
    List<DeviceResponse> GetDevicesByUserId(Guid userId);
    Guid AddDevice(CreateDeviceRequest request);
    void DeleteDeviceById(Guid id);
}