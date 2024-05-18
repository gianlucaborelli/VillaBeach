using Api.Core.Events.Messaging;

namespace Api.Core.Events
{
     public interface IEventStore
    {
        void Save<T>(T theEvent) where T : Event;
    }    
}