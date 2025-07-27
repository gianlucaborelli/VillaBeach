using System.Security.Claims;
using Api.CrossCutting.Identity.Authentication.Model;
using FluentValidation.Results;
using Microsoft.AspNetCore.Identity;

namespace Api.CrossCutting.Identity.JWT.Manager
{
    public interface IJwtAuthManager
    {
        string GenerateAccessToken(AppUser user, IList<string> roles);
        Task<string> GenerateRefreshToken(Guid userId);
        Task<ValidationResult> ValidateRefresToken(string token, string userId);
        ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
    }
}