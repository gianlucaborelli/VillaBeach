namespace Api.Domain.Models
{
    public abstract class OwnerBaseModel: BaseModel
    {        
        public Guid OwnerId { get; set; }
    }
}