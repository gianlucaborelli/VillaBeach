namespace Api.Domain.Dtos.ProductPrice
{
    public class ProductPriceDtoUpdateResult
    {
        public Guid Id { get; set; }
        
        public decimal Value{get;set;}

        public bool Current{get;set;}
                  
        public Guid ProductId {get;set;}
    }
}