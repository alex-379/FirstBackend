namespace FirstBackend.Core.Dtos;

public class DevicesOrders
{
    public Guid DeviceId { get; set; }
    public DeviceDto Device { get; set; }
    public Guid OrderId { get; set; }
    public OrderDto Order { get; set; }
    public int NumberDevices { get; set; }
}
