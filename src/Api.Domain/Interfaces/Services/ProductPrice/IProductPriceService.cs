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

        Task<IEnumerable<ProductPriceDto>> GetAll ();

        Task<ProductPriceDtoCreateResult> Post (ProductPriceDtoCreateRequest user);

        Task<ProductPriceDtoUpdateResult> Put (ProductPriceDtoUpdateRequest user);

        Task<bool> Delete (Guid id);
    }
}