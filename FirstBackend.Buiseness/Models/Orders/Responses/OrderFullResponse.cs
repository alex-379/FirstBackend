using FirstBackend.Buiseness.Models.Devices.Responses;
using FirstBackend.Buiseness.Models.Users.Responses;

namespace FirstBackend.Buiseness.Models.Orders.Responses;

public class OrderFullResponse : OrderResponse
{
    public UserResponse Customer { get; set; }
    public List<DeviceResponse> Devices { get; set; }
}
