using Api.Core.Events.Messaging;
using Api.CrossCutting.Identity.Authentication.Model;
using Api.Domain.Commands.AuthenticationCommands.Validations;

namespace Api.Domain.Commands.AuthenticationCommands
{
    public class ForgetPasswordRequestCommand : Command
    {
        public required string Email { get; set; } 

        public ForgetPasswordRequestCommand( string email)
        {
            Email = email;            
        }

        public override bool IsValid()
        {             
            ValidationResult = new ForgetPasswordRequestCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}