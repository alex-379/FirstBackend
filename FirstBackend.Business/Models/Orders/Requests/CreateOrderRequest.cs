using FirstBackend.Business.Models.Devices.Requests;

namespace FirstBackend.Business.Models.Orders.Requests;

public class CreateOrderRequest
{
    public string Description { get; set; }
    public List<AddDeviceAtOrderRequest> Devices { get; set; }
    public Guid Customer { get; set; }
}
