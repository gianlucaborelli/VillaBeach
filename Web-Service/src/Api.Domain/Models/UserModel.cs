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

        [Required(ErrorMessage = "E-mail é obrigatório.")]
        [EmailAddress(ErrorMessage = "E-mail em formato inválido.")]
        [StringLength(70, ErrorMessage ="E-mail deve ter no maximo {1} caracteres.")]
        [MinLength(5, ErrorMessage = "E-mail deve conter no mínimo {1} caracteres.")]
        private string _email = string.Empty;
        public string Email
        {
            get { return _email; }
            set { _email = value; }
        }

        private byte[] _passwordHash = new byte[32];
        public byte[] PasswordHash
        {
            get { return _passwordHash; }
            set { _passwordHash = value; }
        }

        private byte[] _passwordSalt = new byte[32];
        public byte[] PasswordSalt
        {
            get { return _passwordSalt; }
            set { _passwordSalt = value; }
        }

        private string? _verificationToken;
        public string? VerificationToken
        {
            get { return _verificationToken; }
            set { _verificationToken = value; }
        }

        private DateTime? _verifiedAt;
        public DateTime? VerifiedAt
        {
            get { return _verifiedAt; }
            set { _verifiedAt = value; }
        }

        private string? _passwordResetToken;
        public string? PasswordResetToken
        {
            get { return _passwordResetToken; }
            set { _passwordResetToken = value; }
        }
        
        private DateTime? _resetTokenExpires;
        public DateTime? ResetTokenExpires
        {
            get { return _resetTokenExpires; }
            set { _resetTokenExpires = value; }
        }

        private string _role = "Customer";
        public string Role
        {
            get { return _role; }
            set { _role = value; }
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