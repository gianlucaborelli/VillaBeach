using Api.Domain.Dtos.Login;
using Api.Domain.Dtos.User;

namespace Api.Domain.Interfaces.Services.Login
{
    public interface ILoginService
    {
        Task<ServiceResponse<Guid>> Register(RegisterDtoRequest user);
        Task<bool> UserExists(string email);
        Task<ServiceResponse<string>> Login(string email, string password);
        Task<ServiceResponse<bool>> ChangePassword(string userId, string newPassword);
        int GetUserId();
        string GetUserEmail();
        Task<UserDto> GetUserByEmail(string email);
    }
}