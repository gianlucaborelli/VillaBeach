using Api.Domain.Entities;
using Domain.Dtos.Product;

namespace Api.Domain.Dtos.Purchase
{
    public class PurchaseDto
    {
        public Guid Id { get; set; }
        
        public Guid UserId {get;set;}        

        public bool IsComplete {get; set;} = false;

        public List<PurchasedProductDto> PurchasedProducts {get;set;}
    }
}