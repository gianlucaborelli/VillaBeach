using Api.Domain.Entities;

namespace Api.Domain.Dtos.Purchase
{
    public class PurchaseDto
    {
        public Guid Id { get; set; }
        
        public Guid UserId {get;set;}        

        public bool IsComplete {get; set;} = false;

        public List<PurchasedProduct> PurchasedProducts {get;set;}
    }
}