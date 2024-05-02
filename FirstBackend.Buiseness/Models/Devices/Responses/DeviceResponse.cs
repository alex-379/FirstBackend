using FirstBackend.Core.Enums;

namespace FirstBackend.Buiseness.Models.Devices.Responses;

public class DeviceResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public DeviceType DeviceType { get; set; }
}
