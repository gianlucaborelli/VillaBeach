using Api.Domain.Entities;
using Api.Domain.Interfaces;

namespace Api.Domain.Repository
{
    public interface IUserRepository : IRepository<UserEntity>
    {
        Task<IEnumerable<UserEntity>?> FindByName (string name);

        Task<UserEntity?> FindById (Guid Id);

        Task<UserEntity?> FindByEmail (string email);

        Task<bool> UserExists (string email);
        
        Task<UserEntity?> FindByEmailVerificationToken(string token);

        Task<UserEntity?> FindByForgotPasswordToken(string token);
    }
}