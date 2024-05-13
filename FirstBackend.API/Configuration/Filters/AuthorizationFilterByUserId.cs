using FirstBackend.Core.Constants;
using FirstBackend.Core.Enums;
using FirstBackend.Core.Exсeptions;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Claims;

namespace FirstBackend.API.Configuration.Filters
{
    [AttributeUsage(AttributeTargets.Method)]
    public class AuthorizationFilterByUserId : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var currentUserId = context.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var requestId = context.HttpContext.Request.Path.Value.ToString()[$"{ControllersRoutes.UsersController}/".Length..].Trim();
            if (!context.HttpContext.User.IsInRole(nameof(UserRole.Administrator))
                && currentUserId != requestId)
            {
                throw new UnauthorizedException();
                //context.Result = new StatusCodeResult(StatusCodes.Status403Forbidden);
            }
        }
    }
}