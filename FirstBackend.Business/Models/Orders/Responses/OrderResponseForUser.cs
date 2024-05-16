using FirstBackend.Business.Models.Devices.Responses;

namespace FirstBackend.Business.Models.Orders.Responses;

public class OrderResponseForUser
{
    public string Description { get; set; }
    public List<DeviceForOrderResponse> Devices { get; set; }
}
