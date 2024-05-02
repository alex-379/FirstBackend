using FirstBackend.Buiseness.Models.Devices.Responses;

namespace FirstBackend.Buiseness.Models.Orders.Responses;

public class OrdersByUserResponse : OrderResponse
{
    public List<DeviceResponse> Devices { get; set; }
}
