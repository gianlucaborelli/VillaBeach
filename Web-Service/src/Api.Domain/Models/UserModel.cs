using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Api.Domain.Entities.UserEntityEnum;

namespace Api.Domain.Models
{
    public class UserModel : BaseModel
    {
        [Required(ErrorMessage = "Nome é obrigatório.")]
        [StringLength(70, ErrorMessage ="Nome deve ter no maximo {1} caracteres.")]
        [MinLength(5, ErrorMessage = "Nome deve conter no mínimo {1} caracteres.")]
        private string _name = string.Empty;
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        private UserEmailModel _email = new UserEmailModel();
        public UserEmailModel Email
        {
            get { return _email; }
            set { _email = value; }
        }
        

        private UserAuthenticationModel? _authentication;
        public UserAuthenticationModel? Authentication
        {
            get { return _authentication; }
            set { _authentication = value; }
        }     

        private UserSettingsModel? _settings;
        public UserSettingsModel? Settings
        {
            get { return _settings; }
            set { _settings = value; }
        }              

        private GenderEnum _gender = GenderEnum.RatherNotSay;
        public GenderEnum Gender
        {
            get { return _gender; }
            set { _gender = value; }
        }

        private List<ContactModel> _contactList = new();
        public List<ContactModel> ContactList
        {
            get { return _contactList; }
            set { _contactList = value; }
        }

        private List<AddressModel> _addressList = new();
        public List<AddressModel> AddressList
        {
            get { return _addressList; }
            set { _addressList = value; }
        }

        private List<EnrollmentModel> _enrollmentList = new();
        public List<EnrollmentModel> EnrollmentList
        {
            get { return _enrollmentList; }
            set { _enrollmentList = value; }
        }

        private List<PurchaseModel> _purchasesList = new();
        public List<PurchaseModel> PurchasesList
        {
            get { return _purchasesList; }
            set { _purchasesList = value; }
        }

        private List<SaleModel> _salesList = new();
        public List<SaleModel> SalesList
        {
            get { return _salesList; }
            set { _salesList = value; }
        }
    }
}