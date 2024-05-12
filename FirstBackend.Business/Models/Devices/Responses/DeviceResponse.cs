using FirstBackend.Core.Enums;

namespace FirstBackend.Business.Models.Devices.Responses;

public class DeviceResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public DeviceType Type { get; set; }
}
