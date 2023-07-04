using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Data.Context;
using Api.Data.Repository;
using Api.Domain.Dtos.ProductPrice;
using Api.Domain.Entities;
using Api.Domain.Repository;
using Microsoft.EntityFrameworkCore;

namespace Api.Data.Implementations
{
    public class ProductPriceImplementation : BaseRepository<ProductPriceEntity>, IProductPriceRepository
    {
        private DbSet<ProductPriceEntity> _dataSet;

        public ProductPriceImplementation(MyContext context) : base(context)
        {
            _dataSet = context.Set<ProductPriceEntity>();
        }

        public async Task<IEnumerable<ProductPriceEntity>?> FindByProductId(Guid productId)
        {
            return await _dataSet.Where(u => u.ProductId == productId).ToListAsync();
        }

        public async Task<ProductPriceEntity?> FindCurrentProductPriceByProductId(Guid productId)
        {
            return await _dataSet.FirstOrDefaultAsync(p => p.ProductId == productId && p.Current);
        }

        public async Task<bool> UpdatePricesToNotCurrentByIdProduct(Guid productId)
        {
            using var transaction = _context.Database.BeginTransaction();
            try
            {
                var pricesToUpdate = await _dataSet
                    .Where(p => p.Current && p.ProductId == productId)
                    .ToListAsync();

                pricesToUpdate.ForEach(p => p.Current = false);

                await _context.SaveChangesAsync();

                transaction.Commit();
                return true;
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                throw ex;
            }
        }
    }
}