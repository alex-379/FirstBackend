using FirstBackend.Core.Enums;

namespace FirstBackend.Buiseness.Models.Devices.Requests;

public class CreateDeviceRequest
{
    public string Name { get; set; }
    public DeviceType Type { get; set; }
    public string Address { get; set; }
}
