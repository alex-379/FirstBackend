namespace FirstBackend.Core.Dtos;

public class OrderDto:IdContainer
{
    public string Description { get; set; }

    public UserDto Customer { get; set; }
}
