using Api.Core.Events.Messaging;
using FluentValidation;
using System.ComponentModel.DataAnnotations;

namespace Api.Domain.Commands.UserCommands
{
    public class CreateNewUserCommand: Command
    {        
        public required string Name { get; set; }
        public required string Email { get; set; }

        public CreateNewUserCommand(string name, string email)
        {
            Name = name;
            Email = email;
        }

        public override bool IsValid()
        {
            ValidationResult = new CreateNewUserCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }

        public class CreateNewUserCommandValidation : AbstractValidator<CreateNewUserCommand>
        {
            public CreateNewUserCommandValidation()
            {
                RuleFor(u => u.Name)
                    .NotEmpty().WithMessage("Please ensure you have entered the Name")
                    .Length(3, 150).WithMessage("The Name must have between 3 and 150 characters");

                RuleFor(u => u.Email)
                    .EmailAddress()
                    .NotEmpty().WithMessage("Please ensure you have entered the E-mail")
                    .Length(3, 150).WithMessage("The email must have between 3 and 150 characters");
            }
        }
    }
}