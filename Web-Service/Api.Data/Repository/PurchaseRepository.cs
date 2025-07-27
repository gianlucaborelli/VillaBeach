using Api.Data.Context;
using Api.Core.Data;
using Api.Domain.Entities;
using Api.Domain.Interface;
using Microsoft.EntityFrameworkCore;

namespace Api.Data.Implementations
{
    public class PurchaseRepository : IPurchaseRepository
    {
        protected readonly MyContext Db;
        protected readonly DbSet<Purchase> DbSet;

        public PurchaseRepository(MyContext context)
        {
            Db = context;
            DbSet = Db.Set<Purchase>();
        }

        public IUnitOfWork UnitOfWork => Db;

        public async Task<IEnumerable<Purchase>?> FindByUserId(Guid userId)
        {
            return await DbSet.Where(u => u.UserId == userId).ToListAsync();
        }

        public async Task<bool> ExistAsync(Guid id)
        {
            return await DbSet.AnyAsync(p => p.Id.Equals(id));
        }  

        public async Task<IEnumerable<Purchase>?> SelectAllIncomplete()
        {
            return await DbSet.Where(u => u.IsComplete == false).ToListAsync();
        }

        public async Task<IEnumerable<Purchase>?> SelectAllIncompleteByUser(Guid userId)
        {
            return await DbSet.Where(u => u.UserId == userId && u.IsComplete == false).ToListAsync();
        }                

        public async Task<Purchase?> GetByIdAsync(Guid id)
        {
            return await DbSet.FindAsync(id);
        }

        public async Task<IEnumerable<Purchase>> GetAllAsync()
        {
            return await DbSet.ToListAsync();
        }  

        public void Add(Purchase purchase)
        {
            DbSet.Add(purchase);
        }

        public void Update(Purchase purchase)
        {
            DbSet.Update(purchase);
        }

        public async void Delete(Guid id)
        {
            var purchase = await GetByIdAsync(id);
            DbSet.Remove(purchase!);
        }

        public void Dispose()
        {
            Db.Dispose();
        }
    }
}