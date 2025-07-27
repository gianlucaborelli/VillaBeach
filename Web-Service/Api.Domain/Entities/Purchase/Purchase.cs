using Api.Core.Domain;

namespace Api.Domain.Entities
{
    public class Purchase : Entity, IAggregateRoot
    {   
        public Guid UserId {get;set;}
        public User User { get; set; } 

        public bool IsComplete {get; set;} = false;

        public List<PurchasedProduct> PurchasedProducts {get;set;}
    }
}