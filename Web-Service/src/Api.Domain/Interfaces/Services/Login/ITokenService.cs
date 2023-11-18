using Api.Domain.Entities;

namespace Api.Domain.Interfaces.Services.Login
{
    public interface ITokenService
    {
        string CreateToken(UserEntity user);        
    }
}