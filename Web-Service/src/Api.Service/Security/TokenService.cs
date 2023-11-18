using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Api.Domain.Entities;
using Api.Domain.Interfaces.Services.Login;
using Microsoft.IdentityModel.Tokens;

namespace Api.Service.Security
{
    public class TokenService: ITokenService
    {
        public string CreateToken(UserEntity user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Email),
                new Claim(ClaimTypes.Role, user.Role)
            };

            var tokenKey = Environment.GetEnvironmentVariable("VILLABEACH_TOKEN_KEY");

            if (tokenKey == null)
            {
                throw new ApplicationException("Token key is not configured.");
            }
            
            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8
                .GetBytes(tokenKey));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                    claims: claims,
                    expires: DateTime.Now.AddDays(1),
                    signingCredentials: creds);

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }
    }
}