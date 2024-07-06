using Api.Core.Data;
using Api.Domain.Entities;

namespace Api.Domain.Interface
{
    public interface IUserRepository : IRepository<User>
    {
        Task<List<User>> GetByNameAsync (string name);        

        Task<User?> GetByEmailAsync (string email);

        Task<User?> GetByIdentityIdAsync (Guid identityId);        
    }
}