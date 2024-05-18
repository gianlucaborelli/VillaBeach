namespace Api.Domain.Entities
{
    public class SoldProduct
    {
        public Guid Id { get; set; }

        public int Amount { get; set; }

        public Guid ProductId { get; set; }
        public Product Product { get; set; } = new();

        public Guid ProductPriceId { get; set; }
        public ProductPrice ProductPrice { get; set; } = new();
    }
}