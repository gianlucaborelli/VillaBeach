using Api.Core.Data;
using Api.Domain.Entities;

namespace Api.Domain.Interface
{
    public interface IPurchaseRepository : IRepository<Purchase>
    {
        Task<IEnumerable<Purchase>?> SelectAllIncompleteByUser(Guid userId);
        Task<IEnumerable<Purchase>?> SelectAllIncomplete();
        Task<IEnumerable<Purchase>?> FindByUserId(Guid userId);
    }
}