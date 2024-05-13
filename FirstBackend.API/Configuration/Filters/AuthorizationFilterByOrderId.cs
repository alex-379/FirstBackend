using FirstBackend.Business.Interfaces;
using FirstBackend.Core.Constants;
using FirstBackend.Core.Enums;
using FirstBackend.Core.Exсeptions;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Claims;

namespace FirstBackend.API.Configuration.Filters;

public class AuthorizationFilterByOrderId(IUsersService usersService) : IAuthorizationFilter
{
    private readonly IUsersService _usersService = usersService;

    public void OnAuthorization(AuthorizationFilterContext context)
    {
        var requestId = new Guid(context.HttpContext.Request.Path.Value.ToString()[$"{ControllersRoutes.OrdersController}/".Length..].Trim());
        var userId = _usersService.GetUserIdByOrderId(requestId).ToString();
        var currentUserId = context.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (!context.HttpContext.User.IsInRole(nameof(UserRole.Administrator))
            && currentUserId != userId)
        {
            throw new UnauthorizedException();
        }
    }
}
