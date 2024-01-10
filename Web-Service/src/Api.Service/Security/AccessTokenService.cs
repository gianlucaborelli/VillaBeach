using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Api.Domain.Entities;
using Api.Domain.Interfaces.Services.Authentication;
using Microsoft.IdentityModel.Tokens;

namespace Api.Service.Security
{
    /// <summary>
    /// Service responsible for creating access tokens using JWT (JSON Web Token) for a given UserEntity.
    /// </summary>
    public class AccessTokenService : IAccessTokenService
    {
        /// <summary>
        /// Creates an access token for the specified user entity by generating a JWT (JSON Web Token)
        /// with the user's claims such as NameIdentifier, Name, Email, and Role.
        /// </summary>
        /// <param name="user">The UserEntity for whom the access token is generated.</param>
        /// <returns>A string representation of the generated JWT access token.</returns>
        /// <exception cref="ApplicationException">Thrown when the token key is not configured in the environment variables.</exception>
        public string CreateAccessToken(UserEntity user)
        {
            List<Claim> claims = new()
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Name),
                new Claim(ClaimTypes.Email, user.Email.Address),
                new Claim(ClaimTypes.Role, user.Authentication!.Role)
            };

            var tokenKey = Environment.GetEnvironmentVariable("VILLABEACH_TOKEN_KEY") 
                            ?? throw new ApplicationException("Token key is not configured.");

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8
                .GetBytes(tokenKey));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                    claims: claims,
                    expires: DateTime.Now.AddMinutes(30),
                    signingCredentials: creds);

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }
    }
}