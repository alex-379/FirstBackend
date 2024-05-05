using FirstBackend.Buiseness.Configuration;
using FirstBackend.Buiseness.Interfaces;
using FirstBackend.Buiseness.Models.Tokens.Requests;
using FirstBackend.Buiseness.Models.Users.Responses;
using FirstBackend.Core.Exeptions;
using FirstBackend.DataLayer.Interfaces;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace FirstBackend.Buiseness.Services
{
    public class TokensService(SecretSettings secret, JwtToken jwt, IUsersRepository usersRepository) : ITokensService
    {
        private readonly SecretSettings _secret = secret;
        private readonly JwtToken _jwt = jwt;
        private readonly IUsersRepository _usersRepository = usersRepository;
        private readonly ILogger _logger = Log.ForContext<UsersService>();
        public string GenerateAccessToken(IEnumerable<Claim> claims)
        {
            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secret.SecretToken));
            var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha512);

            var tokenOptions = new JwtSecurityToken(
                issuer: _jwt.ValidIssuer,
                audience: _jwt.ValidAudience,
                claims: claims,
                expires: DateTime.Now.AddDays(_jwt.LifeTimeAccessToken),
                signingCredentials: signinCredentials
            );
            var tokenString = new JwtSecurityTokenHandler().WriteToken(tokenOptions);

            return tokenString;
        }

        public string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);

            return Convert.ToBase64String(randomNumber);
        }

        private ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
        {
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = false,
                ValidateIssuer = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secret.SecretToken)),
                ValidateLifetime = false
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken securityToken);
            if (securityToken is not JwtSecurityToken jwtSecurityToken || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha512, StringComparison.InvariantCultureIgnoreCase))
            {
                throw new SecurityTokenException("Invalid token");
            }

            return principal;
        }

        public AuthenticatedResponse Refresh(RefreshTokenRequest request)
        {
            var principal = GetPrincipalFromExpiredToken(request.AccessToken);
            var mail = principal.FindFirst(ClaimTypes.Email).Value;
            var user = _usersRepository.GetUserByMail(mail);
            if (user is null || user.RefreshToken != request.RefreshToken || user.RefreshTokenExpiryTime <= DateTime.Now)
            {
                throw new ValidationDataException("Ошибка проверки токена пользователя");
            }

            var newAccessToken = GenerateAccessToken(principal.Claims);
            var newRefreshToken = GenerateRefreshToken();
            user.RefreshToken = newRefreshToken;
            _usersRepository.UpdateUser(user);

            return new AuthenticatedResponse()
            {
                Token = newAccessToken,
                RefreshToken = newRefreshToken
            };
        }

        public void Revoke(string mail)
        {
            var user = _usersRepository.GetUserByMail(mail) ?? throw new NotFoundException($"Пользователь с почтой {mail} не найден");
            user.RefreshToken = null;
            _usersRepository.UpdateUser(user);
        }

        public string GetAccessToken(string authorizationHeader)
        {
            string accessToken = string.Empty;
            if (authorizationHeader.ToString().StartsWith("Bearer"))
            {
                accessToken = authorizationHeader.ToString()["Bearer ".Length..].Trim();
            }

            return accessToken;
        }
    }
}
