using Api.Domain.Entities;

namespace Api.Domain.Interfaces.Services.Authentication
{
    public interface IAccessTokenService
    {
        string CreateAccessToken(UserEntity user);        
    }
}