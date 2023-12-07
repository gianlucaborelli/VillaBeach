using Api.Domain.Entities;

namespace Api.Domain.Interfaces.Services.Login
{
    public interface IAccessTokenService
    {
        string CreateAccessToken(UserEntity user);        
    }
}