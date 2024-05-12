using FirstBackend.Business.Models.Tokens.Requests;
using FirstBackend.Business.Models.Users.Responses;
using System.Security.Claims;

namespace FirstBackend.Business.Interfaces;

public interface ITokensService
{
    string GenerateAccessToken(IEnumerable<Claim> claims);
    string GenerateRefreshToken();
    AuthenticatedResponse Refresh(RefreshTokenRequest request);
    void Revoke(string mail);
}