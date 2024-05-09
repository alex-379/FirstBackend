using FirstBackend.Buiseness.Interfaces;
using FirstBackend.Buiseness.Models.Tokens.Requests;
using FirstBackend.Buiseness.Models.Users.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using System.Security.Claims;

namespace FirstBackend.API.Controllers;

[Route("api/tokens")]
[ApiController]
public class TokensController(ITokensService tokensService) : Controller
{
    private readonly ITokensService _tokenService = tokensService;
    private readonly Serilog.ILogger _logger = Log.ForContext<TokensController>();

    [HttpPost]
    [Route("refresh")]
    public ActionResult<AuthenticatedResponse> Refresh([FromBody] RefreshTokenRequest request)
    {
        _logger.Information($"Обновление токена пользователя");
        var authenticatedResponse = _tokenService.Refresh(request);

        return Ok(authenticatedResponse);
    }

    [Authorize(Roles = "Administrator")]
    [HttpPost]
    [Route("revoke")]
    public ActionResult Revoke()
    {
        var mail = User.FindFirst(ClaimTypes.Email).Value;
        _logger.Information($"Отзыв токена пользователя");
        _tokenService.Revoke(mail);

        return NoContent();
    }
}
