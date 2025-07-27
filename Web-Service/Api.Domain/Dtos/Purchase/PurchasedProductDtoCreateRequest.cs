namespace Api.Domain.Dtos.Purchase
{
    public class PurchasedProductDtoCreateRequest
    {
        public Guid ProductId { get; set; }
        public int Amount { get; set; }
        public decimal Price { get; set; }
    }
} 