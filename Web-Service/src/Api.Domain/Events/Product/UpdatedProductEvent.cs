using Api.Core.Events.Messaging;

namespace Api.Domain.Events.Product
{
    public class UpdatedProductEvent : Event
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public string? Description { get; private set; }
        public string? BarCode { get; private set; }
        public decimal Price { get; private set; }
        public int Stock { get; private set; }

        public UpdatedProductEvent(Guid id, string name, string? description, string? barCode, decimal price, int stock)
        {
            Id = id;
            Name = name;
            Description = description;
            BarCode = barCode;
            Price = price;
            Stock = stock;
        }
    }
} 