using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Domain.Entities
{
    public class Address: BaseEntity
    {
        private string _postalCode;
        public string PostalCode
        {
            get { return _postalCode; }
            set { _postalCode = value; }
        }
        
        private string _street;
        public string Street
        {
            get { return _street; }
            set { _street = value; }
        }
        
        private string _number;
        public string Number
        {
            get { return _number; }
            set { _number = value; }
        }

        private string _district;
        public string District
        {
            get { return _district; }
            set { _district = value; }
        }
        
        private string _city;
        public string City
        {
            get { return _city; }
            set { _city = value; }
        }

        private string _state;
        public string State
        {
            get { return _state; }
            set { _state = value; }
        }
        
        private string _description;
        public string Description
        {
            get { return _description; }
            set { _description = value; }
        }   
                
        [ForeignKey("User")]
        public UserEntity User{get;set;}
        public Guid _userId;
    }
}