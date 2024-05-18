using Api.Core.Domain;

namespace Api.Domain.Entities
{
    public class Sale : Entity, IAggregateRoot
    {
        public List<SoldProduct> SoldProducts {get;set;} = new();
        
        public Guid UserId {get;set;}
        public User User { get; set; } = new();
    }
}