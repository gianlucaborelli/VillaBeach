using Api.Core.Events.Messaging;

namespace Api.Domain.Events
{
    public class NewUserRegisteredEvent : Event
    {
        public Guid Id { get; set; }

        public string Name { get; private set; }

        public string Email { get; private set; }

         public NewUserRegisteredEvent(Guid id, string name, string email)
        {
            Id = id;
            Name = name;
            Email = email;
            AggregateId = id;
        }        
    }    
}