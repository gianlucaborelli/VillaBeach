using Api.Core.Domain;

namespace Api.Domain.Entities
{
    public class Product : Entity, IAggregateRoot
    {        
        public string Name{get;set;}

        public string? Description{get;set;}

        public int Stock{get;set;}
        
        public string? BarCode{get;set;}

        public virtual List<ProductPrice> Prices { get; set; } = new();
    }
}