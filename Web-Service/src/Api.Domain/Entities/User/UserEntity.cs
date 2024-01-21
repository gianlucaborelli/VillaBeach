using Api.Domain.Entities.UserEntityEnum;

namespace Api.Domain.Entities
{
    public class UserEntity : BaseEntity
    {
        public string Name { get; set; } = string.Empty;

        public GenderEnum Gender { get; set; } = GenderEnum.RatherNotSay;

        public UserEmailEntity Email { get; set; } = new UserEmailEntity();         

        public UserAuthenticationEntity? Authentication {get; set;}

        public UserSettingsEntity Settings {get;set;} = new UserSettingsEntity();

        public ICollection<ContactEntity>? ContactList { get; set; }

        public ICollection<AddressEntity>? AddressList { get; set; }

        public List<EnrollmentEntity>? EnrollmentList { get; set; }

        public List<PurchaseEntity>? PurchasesList { get; set; }

        public List<SaleEntity>? SalesList { get; set; }
    }    
}