namespace Api.Domain.Entities
{
    public abstract class OwnerBaseEntity: BaseEntity
    {        
        public Guid OwnerId { get; set; }
    }
}