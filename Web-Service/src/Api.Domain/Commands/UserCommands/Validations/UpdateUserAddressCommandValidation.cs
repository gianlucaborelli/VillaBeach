using FluentValidation;

namespace Api.Domain.Commands.UserCommands.Validations
{
    public class UpdateUserAddressCommandValidation: AbstractValidator<UpdateUserAddressCommand>
    {
        public UpdateUserAddressCommandValidation()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Id is required");

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