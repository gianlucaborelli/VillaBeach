using Api.Domain.Dtos.ProductPrice;

namespace Api.Domain.Dtos.Product
{
    public class ProductDtoAvailableResult
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string? Description { get; set; }

        public int Stock{get;set;}

        public string? BarCode { get; set; }

        public ProductPriceDtoAvailableResult Value { get; set; }
    }
}