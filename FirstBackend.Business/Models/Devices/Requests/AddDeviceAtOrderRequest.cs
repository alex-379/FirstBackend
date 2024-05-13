namespace FirstBackend.Business.Models.Devices.Requests;

public class AddDeviceAtOrderRequest
{
    public Guid DeviceId { get; set; }
    public int NumberDevices { get; set; }
}
