using Api.Data.Context;
using Api.Data.Repository;
using Api.Domain.Entities;
using Api.Domain.Repository;
using Microsoft.EntityFrameworkCore;

namespace Api.Data.Implementations
{
    public class UserImplementation : BaseRepository<UserEntity>, IUserRepository
    {
        private DbSet<UserEntity> _dataSet;

        public UserImplementation(MyContext context):base(context)
        {
            _dataSet = context.Set<UserEntity>();
        }

        public async Task<UserEntity?> FindByEmail(string email)
        {
            return await _dataSet.Where(user => user.Email.ToLower()
                 .Equals(email.ToLower())).SingleOrDefaultAsync();
        }

        public async Task<bool> UserExists(string email)
        {
            if (await _dataSet.AnyAsync(user => user.Email.ToLower()
                 .Equals(email.ToLower())))
            {
                return true;
            }
            return false;
        }

        public async Task<IEnumerable<UserEntity>?> FindByName(string name)
        {
            return await _dataSet.Where(u => u.Name.Contains(name)).ToListAsync();
        }

        public async Task<UserEntity?> FindById(Guid id)
        {
            return await _dataSet.Where(user => user.Id == id).SingleOrDefaultAsync();
        }
    }
}