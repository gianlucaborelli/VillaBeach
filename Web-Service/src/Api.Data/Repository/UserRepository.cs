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

        public async Task<bool> ExistsByEmailAsync(string email)
        {
            var result = await DbSet.AnyAsync(user => user.Email.Address.ToLower()
                  .Equals(email.ToLower()));

                  return result;            
        }

        public async Task<bool> ExistAsync(Guid id)
        {
            return await DbSet.AnyAsync(p => p.Id.Equals(id));
        }        

        public Task<IEnumerable<User>?> GetByNameAsync(string name)
        {
            throw new NotImplementedException();
        }

        public async Task<User?> GetByIdAsync(Guid id)
        {
            return await DbSet.FindAsync(id);
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await DbSet.ToListAsync();
        }


        public async Task<User?> GetByEmailAsync(string email)
        {
            return await DbSet.AsNoTracking().FirstOrDefaultAsync(c => c.Email.Address == email);
        }

        public async Task<User?> GetByEmailVerificationTokenAsync(string token)
        {
            return await DbSet.AsNoTracking().FirstOrDefaultAsync(c => c.Email.EmailVerificationToken == token);
        }

        public async Task<User?> GetByForgotPasswordTokenAsync(string token)
        {
            return await DbSet.AsNoTracking().FirstOrDefaultAsync(c => c.Authentication!.ForgotPasswordToken == token);
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