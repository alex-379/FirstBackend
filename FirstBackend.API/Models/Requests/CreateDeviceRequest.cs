using FirstBackend.Core.Enums;

namespace FirstBackend.API.Models.Requests;

public class CreateDeviceRequest
{
    public string Name { get; set; }
    public DeviceType DeviceType { get; set; }
    public string Address { get; set; }
}
