using System.ComponentModel.DataAnnotations;
using Api.Core.Events.Messaging;
using Api.Domain.Commands.UserCommands.Validations;

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
    }
}