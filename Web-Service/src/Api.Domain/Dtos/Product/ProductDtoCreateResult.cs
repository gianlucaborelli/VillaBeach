namespace Api.Domain.Dtos.Product
{
    public class ProductDtoCreateResult
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string? Description { get; set; }

        public string? BarCode { get; set; }

        public decimal Price { get; set; }

        public int Stock { get; set; }
    }
}