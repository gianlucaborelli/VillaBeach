using System.Collections.Generic;

namespace Api.Domain.Dtos.Purchase
{
    public class PurchaseDtoCreateRequest
    {
        public List<PurchasedProductDtoCreateRequest> PurchasedProducts { get; set; }
    }
}