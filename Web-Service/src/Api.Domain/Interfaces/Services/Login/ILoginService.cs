using Api.Domain.Dtos.Login;
using Api.Domain.Dtos.User;

namespace Api.Domain.Interfaces.Services.Login
{
    public interface ILoginService
    {
        Task<Guid> Register(RegisterDtoRequest user);
        Task<LoginDtoResult> Login(string email, string password);
        Task<bool> Logout();
        Task<bool> ChangePassword(string userId, string newPassword);
        Task<RefreshTokenDtoResult> RefreshToken(RefreshTokenDtoRequest request);        
        Guid GetUserId();
        //string GetUserEmail();        
    }
}