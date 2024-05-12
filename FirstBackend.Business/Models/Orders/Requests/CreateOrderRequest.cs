namespace FirstBackend.Business.Models.Orders.Requests;

public class CreateOrderRequest
{
    public string Description { get; set; }
    public List<Guid> Devices { get; set; }
    public Guid Customer { get; set; }
}
