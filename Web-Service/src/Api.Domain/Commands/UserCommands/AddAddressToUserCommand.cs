using Api.Core.Events.Messaging;
using Api.Domain.Commands.UserCommands.Validations;

namespace Api.Domain.Commands.UserCommands
{
    public class AddAddressToUserCommand : Command
    {        
        public required string PostalCode {get; set;}
        public required string Street {get; set;}
        public required string Number{get; set;}
        public required string District{get; set;}
        public required string City{get; set;}
        public required string State{get; set;}
        public string? Description{get; set;}

        public Guid UserId { get; set; }

        public AddAddressToUserCommand(string postalCode, string street, string number, string district, string city, string state, string description)
        {
            PostalCode = postalCode;
            Street = street;
            Number = number;
            District = district;
            City = city;
            State = state;
            Description = description;
        }

        public override bool IsValid()
        {
            ValidationResult = new AddAddressToUserValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}