using FirstBackend.Core.Enums;

namespace FirstBackend.Core.Dtos;

public class DeviceDto : IdContainer
{
    public string Name { get; set; }
    public DeviceType Type { get; set; }
    public string Address { get; set; }
    public bool IsDeleted { get; set; }
    public List<OrderDto> Orders { get; set; }
}
