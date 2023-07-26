using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Domain.Dtos.ProductPrice;

namespace Api.Domain.Interfaces.Services.ProductPrice
{
    public interface IProductPriceService
    {
        Task<ProductPriceDto> Get (Guid id);

        Task<ProductPriceDto> GetCurrentProductPriceByProductId (Guid id);        

        Task<IEnumerable<ProductPriceDto>?> GetAllByProductId (Guid productId);

        Task<ProductPriceDtoCreateResult> Post (ProductPriceDtoCreateRequest productPrice);

        Task<ProductPriceDtoUpdateResult> Put (ProductPriceDtoUpdateRequest productPrice);

        Task<bool> Delete (Guid id);
    }
}