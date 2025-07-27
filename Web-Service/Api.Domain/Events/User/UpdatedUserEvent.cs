using Api.Core.Events.Messaging;
using Api.Domain.Entities.UserEntityEnum;

namespace Api.Domain.Events.User
{
    public class UpdatedUserEvent : Event
    {
        public UpdatedUserEvent(Guid id, string name, string email, GenderEnum gender)
        {
            Id = id;
            Name = name;
            Email = email;
            Gender = gender;
        }

        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public string Email { get; private set; }
        public GenderEnum Gender { get; private set; }        
    }
}