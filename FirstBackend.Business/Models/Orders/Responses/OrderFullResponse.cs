using FirstBackend.Business.Models.Devices.Responses;
using FirstBackend.Business.Models.Users.Responses;

namespace FirstBackend.Business.Models.Orders.Responses;

public class OrderFullResponse : OrderResponse
{
    public UserResponse Customer { get; set; }
    public List<DeviceResponse> Devices { get; set; }
}
