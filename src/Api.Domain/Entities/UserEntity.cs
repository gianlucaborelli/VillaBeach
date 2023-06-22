using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Domain.Entities.UserEntityEnum;

namespace Api.Domain.Entities
{
    public class UserEntity : BaseEntity
    {
        public string Name {get;set;} = string.Empty;

        public string Email {get;set;} = string.Empty;

        public GenderEnum Gender {get;set;} = GenderEnum.RatherNotSay;

        public List<ContactEntity> ContactList {get;set;} = new();

        public List<AddressEntity> AddressList {get;set;} = new();

        public List<EnrollmentEntity> EnrollmentList {get;set;} = new();

        public List<PurchaseEntity> PurchasesList {get;set;} = new();

        public List<SaleEntity> SalesList {get;set;} = new();
    }
}