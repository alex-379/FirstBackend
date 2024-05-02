using FirstBackend.Buiseness.Models.Tokens.Requests;
using FirstBackend.Buiseness.Models.Users.Responses;
using System.Security.Claims;

namespace FirstBackend.Buiseness.Interfaces;

public interface ITokensService
{
    string GenerateAccessToken(IEnumerable<Claim> claims);
    string GenerateRefreshToken();
    AuthenticatedResponse Refresh(RefreshTokenRequest request);
    void Revoke(string username);
    string GetAccessToken(string authorizationHeader);
}