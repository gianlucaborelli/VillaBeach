namespace Api.Domain.Entities
{
    public class ProductPrice
    {
        public Guid Id { get; set;}
        public decimal Value{get;set;}

        public bool Current{get;set;}
                  
        public Guid ProductId {get;set;}

        public Product Product {get;set;}
    }
}