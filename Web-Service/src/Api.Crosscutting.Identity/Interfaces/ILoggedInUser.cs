using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace Api.CrossCutting.Identity.Authentication
{
    public interface ILoggedInUser
    {
        string Name { get; }
        Guid GetUserId();
        string GetUserEmail();
        bool IsAutenticated();
        bool IsInRole(string role);
        IEnumerable<Claim> GetUserClaims();
        HttpContext GetHttpContext();
    }
}