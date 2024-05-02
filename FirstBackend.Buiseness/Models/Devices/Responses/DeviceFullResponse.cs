using FirstBackend.Buiseness.Models.Orders.Responses;

namespace FirstBackend.Buiseness.Models.Devices.Responses;

public class DeviceFullResponse : DeviceResponse
{
    public string Address { get; set; }
    public List<OrderResponse> Orders { get; set; }
}
