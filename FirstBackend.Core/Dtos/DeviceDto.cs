using FirstBackend.Core.Enums;

namespace FirstBackend.Core.Dtos;

public class DeviceDto:IdContainer
{
    public string Name { get; set; }
    public DeviceType DeviceType { get; set; }
    public string Address { get; set; }
    public UserDto Owner { get; set; }
}
