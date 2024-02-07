using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Domain.Entities;
using Api.Domain.Entities.UserEntityEnum;
using Api.Domain.Models;

namespace Api.Domain.Dtos.User
{
    public class UserDto
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public GenderEnum Gender { get; set; } = GenderEnum.RatherNotSay;
        public ICollection<AddressModel> AddressList { get; set; } = new List<AddressModel>();        
        public ICollection<ContactModel> ContactList { get; set; } = new List<ContactModel>();
    }
}