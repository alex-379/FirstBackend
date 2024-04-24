namespace FirstBackend.API.Models.Responses;

public class UserFullResponse : UserResponse
{
    public List<DeviceResponse> Devices { get; set; }
    public List<OrderResponse> Orders { get; set; }
}
