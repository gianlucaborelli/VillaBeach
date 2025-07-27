using Api.Domain.Entities;

namespace Api.Domain.Dtos.Purchase
{
    public class PurchaseDtoUpdateResult
    {
        public Guid Id { get; set; }
        
        public Guid UserId {get;set;}        

        public bool IsComplete {get; set;}

        public List<PurchasedProduct> PurchasedProducts {get;set;}
    }
}