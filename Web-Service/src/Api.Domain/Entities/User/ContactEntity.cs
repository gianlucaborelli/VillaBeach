using Api.Domain.Entities.UserEntityEnum;

namespace Api.Domain.Entities
{
    public class ContactEntity: OwnerBaseEntity
    {        
        public ContactTypeEnum ContactType {get;set;}

        public required string ContactForm {get;set;}
        
        public string? Description {get;set;}
    }
}