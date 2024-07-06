using Api.Domain.Entities;
using Api.Domain.Entities.UserEntityEnum;

namespace Api.Domain.Dtos.User
{
    public class UserView
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public GenderEnum Gender { get; set; } = GenderEnum.RatherNotSay;
        public ICollection<Address> AddressList { get; set; } = new List<Address>();        
        public ICollection<Contact> ContactList { get; set; } = new List<Contact>();
    }
}