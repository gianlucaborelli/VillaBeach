using Api.Data.Context;
using Api.Core.Data;
using Api.Domain.Entities;
using Api.Domain.Interface;
using Microsoft.EntityFrameworkCore;

namespace Api.Data.Implementations
{
    public class ProductRepository : IProductRepository
    {        
        protected readonly MyContext Db;
        protected readonly DbSet<Product> DbSet;

        public ProductRepository(MyContext context)
        {
            Db = context;
            DbSet = Db.Set<Product>();
        }

        public IUnitOfWork UnitOfWork => Db;        

        public async Task<IEnumerable<Product>?> FindByName(string name)
        {
            return await DbSet.Where(u => u.Name.Contains(name)).ToListAsync();
        }

        public async Task<bool> ExistAsync(Guid id)
        {
            return await DbSet.AnyAsync(p => p.Id.Equals(id));
        }  

        public async Task<Product?> GetByIdAsync(Guid id)
        {
            return await DbSet.FindAsync(id);
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            return await DbSet.ToListAsync();
        }  

        public void Add(Product product)
        {
            DbSet.Add(product);
        }

        public void Update(Product product)
        {
            DbSet.Update(product);
        }

        public async void Delete(Guid id)
        {
            var product = await GetByIdAsync(id);
            DbSet.Remove(product!);
        }

        public void Dispose()
        {
            Db.Dispose();
        }
    }
}