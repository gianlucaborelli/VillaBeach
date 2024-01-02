using Api.Domain.Dtos.Login;

namespace Api.Domain.Interfaces.Services.Authentication
{
    public interface IAuthenticationService
    {
        Task<Guid> Register(RegisterDtoRequest user);
        Task<LoginDtoResult> Login(string email, string password);
        Task<bool> Logout();
        Task<bool> EmailVerificationToken(string emailVerificationToken);
        Task<bool> ChangePassword(string newPassword);
        Task<RefreshTokenDtoResult> RefreshToken(RefreshTokenDtoRequest request);        
        Guid GetUserId();
        string GetUserEmail();        
    }
}