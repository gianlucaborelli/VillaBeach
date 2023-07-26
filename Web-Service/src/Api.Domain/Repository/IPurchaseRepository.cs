using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Domain.Entities;
using Api.Domain.Interfaces;

namespace Api.Domain.Repository
{
    public interface IPurchaseRepository : IRepository<PurchaseEntity>
    {
        Task<IEnumerable<PurchaseEntity>?> SelectAllIncompleteByUser(Guid userId);
        Task<IEnumerable<PurchaseEntity>?> SelectAllIncomplete();
        Task<IEnumerable<PurchaseEntity>?> FindByUserId(Guid userId);
        Task<PurchaseEntity?> SetComplete(Guid purchaseId);
        Task<PurchaseEntity?> SetIncomplete(Guid purchaseId);
    }
}