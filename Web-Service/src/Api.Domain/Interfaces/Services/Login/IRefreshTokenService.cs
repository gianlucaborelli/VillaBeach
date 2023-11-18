using System.Security.Claims;
using Api.Domain.Dtos.Login;

namespace Api.Domain.Interfaces.Services.Login
{
    public interface IRefreshTokenService
    {
        RefreshTokenDto GenerateRefreshToken();
        ClaimsPrincipal? GetPrincipalFromExpiredToken(string? token);
    }    
}