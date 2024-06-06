using Api.Core.Data;
using Api.Domain.Entities;

namespace Api.Domain.Interface
{
    public interface IUserRepository : IRepository<User>
    {
        //Task<bool> ExistsByEmailAsync (string email);

        Task<IEnumerable<User>?> GetByNameAsync (string name);        

        // Task<User?> GetByEmailAsync (string email);        
        
        // Task<User?> GetByEmailVerificationTokenAsync(string token);

        // Task<User?> GetByForgotPasswordTokenAsync(string token);
    }
}