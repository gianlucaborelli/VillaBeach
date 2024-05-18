
using System.Security.Claims;
using Api.CrossCutting.Identity.Authentication.Model;

namespace Api.CrossCutting.Identity.Authentication
{
    public interface IAuthenticationService
    {
        Guid GetUserId(); 
        string GetUserEmail();
        void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt);
        bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt);
        string CreateAccessToken(Guid userId, string userName, string userEmail, string userRole);
        RefreshToken GenerateRefreshToken();
        ClaimsPrincipal? GetPrincipalFromExpiredToken(string? token);
    }
}