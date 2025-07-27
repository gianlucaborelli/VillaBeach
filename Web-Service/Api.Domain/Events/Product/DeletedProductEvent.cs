using Api.Core.Events.Messaging;

namespace Api.Domain.Events.Product
{
    public class DeletedProductEvent : Event
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }

        public DeletedProductEvent(Guid id, string name)
        {
            Id = id;
            Name = name;
        }
    }
} 