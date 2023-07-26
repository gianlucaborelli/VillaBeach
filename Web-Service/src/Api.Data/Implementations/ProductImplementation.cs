using Api.Data.Context;
using Api.Data.Repository;
using Api.Domain.Dtos.Product;
using Api.Domain.Entities;
using Api.Domain.Repository;
using Microsoft.EntityFrameworkCore;

namespace Api.Data.Implementations
{
    public class ProductImplementation : BaseRepository<ProductEntity>, IProductRepository
    {
        private DbSet<ProductEntity> _dataSet;

        public ProductImplementation(MyContext context) : base(context)
        {
            _dataSet = context.Set<ProductEntity>();
        }

        public async Task<IEnumerable<ProductEntity>?> FindByName(string name)
        {
            return await _dataSet.Where(u => u.Name.Contains(name)).ToListAsync();
        }
    }
}