using FirstBackend.Core.Enums;

namespace FirstBackend.Core.Dtos;

public class UserDto:IdContainer
{
    public string Name { get; set; }
    public string Mail { get; set; }
    public string Password { get; set; }
    public UserRole Role { get; set; }
    public string RefreshToken { get; set; }
    public DateTime RefreshTokenExpiryTime { get; set; }
    public bool IsDeleted { get; set; }
    public List<OrderDto> Orders {  get; set; }
}
