using Api.Core.Events.Messaging;

namespace Api.Domain.Events.User
{
    public class AddedAddressToUserEvent : Event
    {

        public Guid AddressId { get; private set; }
        public string PostalCode { get; private set; }
        public string Street { get; private set; }
        public string Number { get; private set; }
        public string District { get; private set; }
        public string City { get; private set; }
        public string State { get; private set; }
        public string? Description { get; private set; }

        public AddedAddressToUserEvent(Guid userId, Guid addressId, string postalCode, string street, string number, string district, string city, string state, string description)
        {
            AggregateId = userId;
            AddressId = addressId;
            PostalCode = postalCode;
            Street = street;
            Number = number;
            District = district;
            City = city;
            State = state;
            Description = description;
        }
    }
}