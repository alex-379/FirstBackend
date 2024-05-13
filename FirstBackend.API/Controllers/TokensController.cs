using FirstBackend.Business.Interfaces;
using FirstBackend.Business.Models.Tokens.Requests;
using FirstBackend.Business.Models.Users.Responses;
using FirstBackend.Core.Constants;
using FirstBackend.Core.Constants.Logs.API;
using FirstBackend.Core.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using System.Security.Claims;

namespace FirstBackend.API.Controllers;

[Route(ControllersRoutes.TokensController)]
[ApiController]
public class TokensController(ITokensService tokensService) : Controller
{
    private readonly ITokensService _tokenService = tokensService;
    private readonly Serilog.ILogger _logger = Log.ForContext<TokensController>();

    [HttpPost]
    [Route(ControllersRoutes.Refresh)]
    public ActionResult<AuthenticatedResponse> Refresh([FromBody] RefreshTokenRequest request)
    {
        _logger.Information(TokensControllerLogs.Refresh);
        var authenticatedResponse = _tokenService.Refresh(request);

        return Ok(authenticatedResponse);
    }

    [Authorize(Roles = nameof(UserRole.Administrator))]
    [HttpPost]
    [Route(ControllersRoutes.Revoke)]
    public ActionResult Revoke()
    {
        var mail = User.FindFirst(ClaimTypes.Email).Value;
        _logger.Information(TokensControllerLogs.Revoke);
        _tokenService.Revoke(mail);

        return NoContent();
    }
}
