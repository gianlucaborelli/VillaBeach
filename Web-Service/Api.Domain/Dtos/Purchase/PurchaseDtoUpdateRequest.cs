using System.Collections.Generic;

namespace Api.Domain.Dtos.Purchase
{
    public class PurchaseDtoUpdateRequest
    {
        public Guid Id { get; set; }
        public List<PurchasedProductDtoCreateRequest> PurchasedProducts { get; set; }
    }
}