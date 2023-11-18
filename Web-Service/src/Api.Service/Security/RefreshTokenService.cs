using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Api.Domain.Dtos.Login;
using Api.Domain.Interfaces.Services.Login;
using Microsoft.IdentityModel.Tokens;

namespace Api.Service.Security
{
    public class RefreshTokenService : IRefreshTokenService
    {

        public RefreshTokenDto GenerateRefreshToken()
        {
            var refreshToken = new RefreshTokenDto
            {
                Token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64)),
                Expires = DateTime.UtcNow.AddDays(7),
                Created = DateTime.Now
            };

            return refreshToken;
        }

        public ClaimsPrincipal? GetPrincipalFromExpiredToken(string? token)
        {
            var tokenKey = Environment.GetEnvironmentVariable("VILLABEACH_TOKEN_KEY");

            if (tokenKey == null)
            {
                throw new ApplicationException("Token key is not configured.");
            }

            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = false,
                ValidateIssuer = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8
                                   .GetBytes(tokenKey)),
                ValidateLifetime = false
            };

            var tokenHandler = new JwtSecurityTokenHandler();


            
            SecurityToken securityToken;
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out securityToken);
            var jwtSecurityToken = securityToken as JwtSecurityToken;
            if (jwtSecurityToken == null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha512Signature, StringComparison.InvariantCultureIgnoreCase))
                throw new SecurityTokenException("Invalid token");

            return principal;
        }
    }
}