namespace FirstBackend.Core.Constants;

public static class ControllersRoutes
{
    public const string UsersController = "/api/users";
    public const string TokensController = "api/tokens";
    public const string OrdersController = "/api/orders";
    public const string DevicesController = "/api/devices";
    public const string Id = "{id}";
    public const string DevicesByUserId = "{userId}/devices";
    public const string OrdersByUserId = "{userId}/orders";
    public const string Login = "login";
    public const string UserPassword = "{id}/password";
    public const string UserMail = "{id}/mail";
    public const string UserRole = "{id}/role";
    public const string Refresh = "refresh";
    public const string Revoke = "revoke";
}
