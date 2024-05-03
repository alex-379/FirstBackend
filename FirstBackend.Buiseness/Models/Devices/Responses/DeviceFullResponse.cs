using FirstBackend.Buiseness.Models.Orders.Responses;

namespace FirstBackend.Buiseness.Models.Devices.Responses;

public class DeviceFullResponse : DeviceResponse
{
    public string Address { get; set; }
    public int NumberOrders { get; set; }
}
