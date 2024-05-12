using FirstBackend.Business.Models.Devices.Responses;

namespace FirstBackend.Business.Models.Orders.Responses;

public class OrdersByUserResponse : OrderResponse
{
    public List<DeviceResponse> Devices { get; set; }
}
