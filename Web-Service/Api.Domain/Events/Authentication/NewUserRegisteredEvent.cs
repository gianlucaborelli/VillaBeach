using Api.Core.Events.Messaging;

namespace Api.Domain.Events
{
    public class NewUserRegisteredEvent : Event
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Email { get;  set; }

        public string TokenUrl { get; set;}

        public NewUserRegisteredEvent(Guid id, string name, string email, string tokenUrl)
        {
            Id = id;
            Name = name;
            Email = email;
            TokenUrl = tokenUrl;
            AggregateId = id;            
        }
    }
}