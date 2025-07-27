using Api.Core.Events.Messaging;

namespace Api.Domain.Events.User
{
    public class CreatedNewUserEvent: Event
    {
        public Guid Id { get; set; }
        public string Name { get; private set; }
        public string Email { get; private set; }

        public CreatedNewUserEvent(Guid id, string name, string email)
        {
            Id = id;
            Name = name;
            Email = email;
        }
    }
}