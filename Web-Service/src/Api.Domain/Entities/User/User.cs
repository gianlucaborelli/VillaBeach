using Api.Domain.Entities.UserEntityEnum;
using Api.Core.Domain;

namespace Api.Domain.Entities
{
    public class User : Entity, IAggregateRoot
    {
        public string Name { get; set; } = string.Empty;

        public GenderEnum Gender { get; set; } = GenderEnum.RatherNotSay;

        public Email Email { get; set; } = new Email();         

        public Authentication? Authentication {get; set;}

        public Settings Settings {get;set;} = new Settings();

        public ICollection<Contact>? ContactList { get; set; }

        public ICollection<Address>? AddressList { get; set; }

        public List<Enrollment>? EnrollmentList { get; set; }

        public List<Purchase>? PurchasesList { get; set; }

        public List<Sale>? SalesList { get; set; }
    }    
}