using Api.Core.Events.Messaging;
using FluentValidation;

namespace Api.Domain.Commands.UserCommands
{
    public class UpdateUserAddressCommand : Command
    {
        public Guid AddressId { get; set; }
        
        public required string PostalCode { get; set; }
        public required string Street { get; set; }
        public required string Number { get; set; }
        public required string District { get; set; }
        public required string City { get; set; }
        public required string State { get; set; }
        public string? Description { get; set; }

        public Guid UserId { get; set; }

        public UpdateUserAddressCommand () { }
        public UpdateUserAddressCommand(Guid addressId, Guid userId, string postalCode, string street, string number, string district, string city, string state, string description)
        {
            AddressId = addressId;
            PostalCode = postalCode;
            Street = street;
            Number = number;
            District = district;
            City = city;
            State = state;
            Description = description;
            UserId = userId;
        }

        public override bool IsValid()
        {
            ValidationResult = new UpdateUserAddressCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }

        public class UpdateUserAddressCommandValidation : AbstractValidator<UpdateUserAddressCommand>
        {
            public UpdateUserAddressCommandValidation()
            {
                RuleFor(x => x.AddressId)
                    .NotEmpty().WithMessage("Address Id is required");

                RuleFor(x => x.PostalCode)
                    .NotEmpty().WithMessage("PostalCode is required")
                    .Length(8).WithMessage("PostalCode must have 8 characters");

                RuleFor(x => x.Street)
                    .NotEmpty().WithMessage("Street is required")
                    .Length(2, 100).WithMessage("Street must have between 2 and 100 characters");

                RuleFor(x => x.Number)
                    .NotEmpty().WithMessage("Number is required")
                    .Length(1, 10).WithMessage("Number must have between 1 and 10 characters");

                RuleFor(x => x.District)
                    .NotEmpty().WithMessage("District is required")
                    .Length(2, 100).WithMessage("District must have between 2 and 100 characters");

                RuleFor(x => x.City)
                    .NotEmpty().WithMessage("City is required")
                    .Length(2, 100).WithMessage("City must have between 2 and 100 characters");

                RuleFor(x => x.State)
                    .NotEmpty().WithMessage("State is required")
                    .Length(2, 100).WithMessage("State must have 2 characters");

                RuleFor(x => x.Description)
                    .Length(2, 100).WithMessage("Description must have between 2 and 200 characters");
            }
        }
    }
}