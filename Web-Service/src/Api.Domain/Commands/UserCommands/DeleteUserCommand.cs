using Api.Core.Events.Messaging;
using FluentValidation;

namespace Api.Domain.Commands.UserCommands
{
    public class DeleteUserCommand : Command
    {
        public Guid Id { get; set; }        

        public DeleteUserCommand(Guid id)
        {
            Id = id;
        }

        public override bool IsValid()
        {
            ValidationResult = new DeleteUserCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }

        public class DeleteUserCommandValidation : AbstractValidator<DeleteUserCommand>
        {
            public DeleteUserCommandValidation()
            {
                RuleFor(u => u.Id)
                    .NotEqual(Guid.Empty);
            }
        }
    }
}