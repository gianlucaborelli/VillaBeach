using Api.Domain.Dtos.Login;
using Api.Domain.Dtos.User;

namespace Api.Domain.Interfaces.Services.Login
{
    public interface ILoginService
    {
        Task<ServiceResponse<Guid>> Register(RegisterDtoRequest user);
        Task<bool> UserExists(string email);
        Task<LoginDtoResult> Login(string email, string password);
        Task<ServiceResponse<bool>> ChangePassword(string userId, string newPassword);
        Task<RefreshTokenDtoResult> RefreshToken(RefreshTokenDtoRequest request);
        int GetUserId();
        string GetUserEmail();
        Task<UserDto> GetUserByEmail(string email);
    }
}