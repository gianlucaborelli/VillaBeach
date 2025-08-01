namespace Api.Domain.Entities
{
    public class PurchasedProduct
    {        
        public Guid Id { get; set; }
        public int Amount {get;set;}

        public Guid ProductId {get;set;}
        public Product Product { get; set; }

        public decimal Price { get; set; }
    }
}