
using Api.Domain.Entities;
using Api.Domain.Interfaces;

namespace Api.Domain.Repository
{
    public interface IProductPriceRepository: IRepository<ProductPriceEntity>
    {
        Task<IEnumerable<ProductPriceEntity>?> FindByProductId (Guid productId);
        
        Task<ProductPriceEntity?> FindCurrentProductPriceByProductId (Guid productId);

        Task<bool> UpdatePricesToNotCurrentByIdProduct(Guid productIds);
    }
}