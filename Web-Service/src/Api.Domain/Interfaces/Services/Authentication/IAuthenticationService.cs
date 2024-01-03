using Api.Domain.Dtos.Login;

namespace Api.Domain.Interfaces.Services.Authentication
{
    public interface IAuthenticationService
    {
        Guid GetUserId();
        string GetUserEmail();      
        Task<Guid> Register(RegisterDtoRequest user);
        Task EmailVerificationToken(string emailVerificationToken);
        Task<LoginDtoResult> Login(string email, string password);
        Task<bool> Logout();        
        Task<bool> ChangePassword(string newPassword);
        Task<RefreshTokenDtoResult> RefreshToken(RefreshTokenDtoRequest request);
        Task ForgotPasswordRequest(string userEmail);          
    }
}