using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Domain.Dtos.ProductPrice;
using Api.Domain.Entities;
using Api.Domain.Interfaces;

namespace Api.Domain.Repository
{
    public interface IProductPriceRepository: IRepository<ProductPriceEntity>
    {
        Task<IEnumerable<ProductPriceEntity>?> FindByProductId (Guid productId);
        
        Task<ProductPriceEntity?> FindCurrentProductPriceByProductId (Guid productId);
    }
}