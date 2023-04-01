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

        public List<ContactEntity> ContactList
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
        private List<ContactEntity> _contactList = new();


    }
}