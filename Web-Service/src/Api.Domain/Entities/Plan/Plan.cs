using Api.Core.Domain;

namespace Api.Domain.Entities
{
    public class Plan : Entity, IAggregateRoot
    {
        
        public string Name{get; set;}        
        
        public int AmountOfDay{get; set;}        
        
        public string? Description{get; set;}

        public virtual List<PlanPrice> Prices { get; set; } = new();
    }
}