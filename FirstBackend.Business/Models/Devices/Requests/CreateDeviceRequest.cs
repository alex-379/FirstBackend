using FirstBackend.Core.Enums;

namespace FirstBackend.Business.Models.Devices.Requests;

public class CreateDeviceRequest
{
    public string Name { get; set; }
    public DeviceType Type { get; set; }
    public string Address { get; set; }
}
