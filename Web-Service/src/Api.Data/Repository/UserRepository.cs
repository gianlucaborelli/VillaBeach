using Api.Core.Data;
using Api.Data.Context;
using Api.Domain.Entities;
using Api.Domain.Interface;
using Microsoft.EntityFrameworkCore;

namespace Api.Data.Repository
{
    public class UserRepository : IUserRepository
    {
        protected readonly MyContext Db;
        protected readonly DbSet<User> DbSet;

        public UserRepository(MyContext context)
        {
            Db = context;
            DbSet = Db.Set<User>();
        }

        public IUnitOfWork UnitOfWork => Db;

        public async Task<bool> ExistAsync(Guid id)
        {
            return await DbSet.AnyAsync(p => p.Id.Equals(id));
        }    

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await DbSet.ToListAsync();
        }    

        public async Task<List<User>> GetByNameAsync(string name)
        {
            return await DbSet.AsNoTracking().Where(u => u.Name.Contains(name)).ToListAsync();
        }

        public async Task<User?> GetByIdAsync(Guid id)
        {
            return await DbSet.FindAsync(id);
        }

        public async Task<User?> GetByIdentityIdAsync(Guid identityId)
        {
            return await DbSet.AsNoTracking().FirstOrDefaultAsync(u  => u.IdentityId.Equals(identityId));
        }        

        public void Add(User user)
        {
             DbSet.Add(user);
        }        

        public void Update(User user)
        {
            DbSet.Update(user);
        }

        public async void Delete(Guid id)
        {
            var user = await GetByIdAsync(id);
            DbSet.Remove(user!);
        }

        public void Dispose()
        {
            Db.Dispose();
        }
    }
}