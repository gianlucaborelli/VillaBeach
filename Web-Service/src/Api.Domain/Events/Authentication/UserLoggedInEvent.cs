using Api.Core.Events.Messaging;

namespace Api.Domain.Events
{
    class UserLoggedInEvent : Event
    {
        public Guid Id { get; set; }

        public string Name { get; private set; }

        public string Email { get; private set; }

        public string Ip { get; private set; }

        public string Device { get; private set; }

        public UserLoggedInEvent(Guid id, string name, string email, string ip, string device )
        {
            Id = id;
            Name = name;
            Email = email;
            Ip = ip;
            Device = device;
            AggregateId = id;
        }
    }
}