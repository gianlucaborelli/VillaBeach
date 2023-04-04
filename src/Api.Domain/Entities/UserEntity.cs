using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Domain.Entities.UserEntityEnum;

namespace Api.Domain.Entities
{
    public class UserEntity : BaseEntity
    {
        public string Name 
        { 
            get
            {
                return _name;
            } 
            set
            {
                _name = value;
            }
        }
        private string _name = string.Empty;

        public string Email 
        { 
            get
            {
                return _email;
            }
            set
            {
                _email = value;
            }
        }
        private string _email = string.Empty;

        public GenderEnum Gender 
        {
            get
            {
                return _gender;
            } 
            set
            {
                _gender = value;
            }
        }
        private GenderEnum _gender = GenderEnum.RatherNotSay;

        public List<Contact> ContactList
        {
            get
            {
                return _contactList;
            }
            set
            {
                _contactList.AddRange(value);
            }
        }
        private List<Contact> _contactList = new();

        public List<Address> AddressList
        {
            get
            {
                return _addressList;
            }
            set
            {
                _addressList.AddRange(value);
            }
        }
        private List<Address> _addressList = new();

        public List<Enrollment> EnrollmentList
        {
            get
            {
                return _enrollmentList;
            }
            set
            {
                _enrollmentList.AddRange(value);
            }
        }
        private List<Enrollment> _enrollmentList = new();

        public List<Purchase> PurchasesList
        {
            get
            {
                return _purchasesList;
            }
            set
            {
                _purchasesList.AddRange(value);
            }
        }
        private List<Purchase> _purchasesList = new();

        public List<Sale> SalesList
        {
            get
            {
                return _salesList;
            }
            set
            {
                _salesList.AddRange(value);
            }
        }
        private List<Sale> _salesList = new();
    }
}