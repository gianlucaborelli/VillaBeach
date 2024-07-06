using Api.Domain.Entities.UserEntityEnum;
using Api.Core.Domain;

namespace Api.Domain.Entities
{
    public class User : Entity, IAggregateRoot
    {
        public required string Name { get; set; }

        public Guid? IdentityId { get; set; }

        public required string Email { get; set; }

        public GenderEnum Gender { get; set; } = GenderEnum.RatherNotSay;

        public Settings Settings { get; set; } = new Settings();

        public ICollection<Contact>? ContactList { get; set; }

        public ICollection<Address>? AddressList { get; set; }

        public List<Enrollment>? EnrollmentList { get; set; }

        public List<Purchase>? PurchasesList { get; set; }

        public List<Sale>? SalesList { get; set; }
    }
}