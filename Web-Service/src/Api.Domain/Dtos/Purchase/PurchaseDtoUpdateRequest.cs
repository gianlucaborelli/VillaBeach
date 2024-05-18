namespace Api.Domain.Dtos.Purchase
{
    public class PurchaseDtoUpdateRequest
    {
        public Guid Id { get; set; }
        
        public Guid UserId {get;set;}        

        public bool IsComplete {get; set;}
    }
}