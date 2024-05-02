using FirstBackend.Buiseness.Interfaces;
using FirstBackend.Buiseness.Models.Tokens.Requests;
using FirstBackend.Buiseness.Models.Users.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace FirstBackend.API.Controllers;

[Route("api/tokens")]
[ApiController]
public class TokensController(ITokensService tokensService) : Controller
{
    private readonly ITokensService _tokenService = tokensService;
    private readonly Serilog.ILogger _logger = Log.ForContext<UsersController>();

    [HttpPost]
    [Route("refresh")]
    public ActionResult<AuthenticatedResponse> Refresh([FromBody] RefreshTokenRequest request)
    {
        _logger.Information($"Обновление токена пользователя");
        var authenticatedResponse = _tokenService.Refresh(request);

        return Ok(authenticatedResponse);
    }

    [HttpPost, Authorize]
    [Route("revoke")]
    public ActionResult Revoke()
    {
        var username = User.Identity.Name;
        _logger.Information($"Отзыв токена пользователя");
        _tokenService.Revoke(username);

        return NoContent();
    }
}
