using FirstBackend.Buiseness.Models.Devices.Responses;
using FirstBackend.Buiseness.Models.Orders.Responses;

namespace FirstBackend.Buiseness.Models.Users.Responses;

public class UserFullResponse : UserResponse
{
    public List<DeviceResponse> Devices { get; set; }
    public List<OrderResponse> Orders { get; set; }
}
