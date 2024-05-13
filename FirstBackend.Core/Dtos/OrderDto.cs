namespace FirstBackend.Core.Dtos;

public class OrderDto : IdContainer
{
    public string Description { get; set; }
    public UserDto Customer { get; set; }
    public bool IsDeleted { get; set; }
    public List<DeviceDto> Devices { get; set; }
    public List<DevicesOrders> DevicesOrders { get; set; }
}
