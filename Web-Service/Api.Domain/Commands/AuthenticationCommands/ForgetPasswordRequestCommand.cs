using Api.Core.Events.Messaging;
using FluentValidation;

namespace Api.Domain.Commands.AuthenticationCommands
{
    public class ForgetPasswordRequestCommand : Command
    {
        public required string Email { get; set; }

        public ForgetPasswordRequestCommand() { }

        public ForgetPasswordRequestCommand( string email)
        {
            Email = email;            
        }

        public override bool IsValid()
        {             
            ValidationResult = new ForgetPasswordRequestCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }

        public class ForgetPasswordRequestCommandValidation : AbstractValidator<ForgetPasswordRequestCommand>
        {
            public ForgetPasswordRequestCommandValidation()
            {
                RuleFor(u => u.Email)
                    .NotEmpty().WithMessage("Please ensure you have entered the Email");
            }
        }
    }
}