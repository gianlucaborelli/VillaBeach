using Api.Core.Events;
using Api.Core.Events.Messaging;
using Api.CrossCutting.Identity.Authentication;
using Api.Data.Repository.EventSourcing;
using Newtonsoft.Json;

namespace Api.Data.EventSourcing
{
    public class SqlEventStore : IEventStore
    {
        private readonly IEventStoreRepository _eventStoreRepository;        
        private readonly ILoggedInUser _user;

        public SqlEventStore(IEventStoreRepository eventStoreRepository, ILoggedInUser user)
        {
            _eventStoreRepository = eventStoreRepository;
            _user = user;
        }

        public void Save<T>(T theEvent) where T : Event
        {
            var serializedData = JsonConvert.SerializeObject(theEvent);

            var storedEvent = new StoredEvent(
                theEvent,
                serializedData,
                _user.Name ?? _user.GetUserEmail()
                );

            _eventStoreRepository.Store(storedEvent);
        }
    }
}