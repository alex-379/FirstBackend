using FirstBackend.Core.Enums;

namespace FirstBackend.API.Models.Responses;

public class DeviceResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public DeviceType DeviceType { get; set; }
    public string Address { get; set; }
}
