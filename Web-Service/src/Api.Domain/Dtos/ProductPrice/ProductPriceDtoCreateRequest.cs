using Api.Domain.Dtos.Attributes;

namespace Api.Domain.Dtos.ProductPrice
{
    public class ProductPriceDtoCreateRequest
    {        
        [DecimalPrecision(2)]
        public decimal Value{get;set;}
        
        public Guid ProductId {get;set;}

    }
}