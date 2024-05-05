using FirstBackend.Buiseness.Models.Devices.Requests;
using FirstBackend.Buiseness.Models.Devices.Responses;

namespace FirstBackend.Buiseness.Interfaces;

public interface IDevicesService
{
    List<DeviceResponse> GetAllDevices();
    DeviceFullResponse GetDeviceById(Guid id);
    List<DeviceResponse> GetDevicesByUserId(Guid userId);
    Guid AddDevice(CreateDeviceRequest request);
    void DeleteDeviceById(Guid id);
}