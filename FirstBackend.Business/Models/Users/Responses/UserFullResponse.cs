using FirstBackend.Business.Models.Devices.Responses;
using FirstBackend.Business.Models.Orders.Responses;

namespace FirstBackend.Business.Models.Users.Responses;

public class UserFullResponse : UserResponse
{
    public List<DeviceResponse> Devices { get; set; }
    public List<OrderResponse> Orders { get; set; }
}
