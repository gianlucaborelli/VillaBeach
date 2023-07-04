using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Data.Context;
using Api.Data.Repository;
using Api.Domain.Entities;
using Api.Domain.Repository;
using Microsoft.EntityFrameworkCore;

namespace Api.Data.Implementations
{
    public class PurchaseImplementation : BaseRepository<PurchaseEntity>, IPurchaseRepository
    {
        private DbSet<PurchaseEntity> _dataSet;
        public PurchaseImplementation(MyContext context) : base(context)
        {
            _dataSet = context.Set<PurchaseEntity>();
        }

        public async Task<IEnumerable<PurchaseEntity>?> FindByUserId(Guid userId)
        {
            return await _dataSet.Where(u => u.UserId == userId).ToListAsync();
        }

        public async Task<IEnumerable<PurchaseEntity>?> SelectAllIncomplete()
        {
            return await _dataSet.Where(u => u.IsComplete == false).ToListAsync();
        }

        public async Task<IEnumerable<PurchaseEntity>?> SelectAllIncompleteByUser(Guid userId)
        {
            return await _dataSet.Where(u => u.UserId == userId && u.IsComplete == false).ToListAsync();
        }

        public async Task<PurchaseEntity> SetComplete(Guid purchaseId)
        {
            var purchase = await _dataSet.FirstOrDefaultAsync(u => u.Id == purchaseId);

            if (purchase != null)
            {
                purchase.IsComplete = true;
                await _context.SaveChangesAsync();                
            }

            return purchase;
        }
    }
}