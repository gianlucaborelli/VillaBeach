using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Domain.Entities.UserEntityEnum;

namespace Api.Domain.Entities
{
    public class UserEntity : BaseEntity
    {
        public string Name { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public byte[] PasswordHash { get; set; } = new byte[32];

        public byte[] PasswordSalt { get; set; } = new byte[32];

        public string? VerificationToken { get; set; }

        public DateTime? VerifiedAt { get; set; }

        public string? PasswordResetToken { get; set; }

        public DateTime? ResetTokenExpires { get; set; }

        public string RefreshToken { get; set; } = string.Empty;

        public DateTime? RefreshTokenExpires {get; set;}

        public string Role { get; set; } = "Customer";

        public GenderEnum Gender { get; set; } = GenderEnum.RatherNotSay;

        public List<ContactEntity> ContactList { get; set; }

        public List<AddressEntity> AddressList { get; set; }

        public List<EnrollmentEntity> EnrollmentList { get; set; }

        public List<PurchaseEntity> PurchasesList { get; set; }

        public List<SaleEntity> SalesList { get; set; }
    }
}