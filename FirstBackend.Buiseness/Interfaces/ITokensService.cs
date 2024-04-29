using System.Security.Claims;

namespace FirstBackend.Buiseness.Interfaces;

public interface ITokensService
{
    string GenerateAccessToken(string secretToken, IEnumerable<Claim> claims);
    string GenerateRefreshToken();
    ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
}