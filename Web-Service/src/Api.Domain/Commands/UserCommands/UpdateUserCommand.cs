using Api.Core.Events.Messaging;
using Api.Domain.Commands.UserCommands.Validations;
using Api.Domain.Entities.UserEntityEnum;

namespace Api.Domain.Commands.UserCommands
{
    public class UpdateUserCommand : Command
    {
        public required Guid Id { get; set; }
        public required string Name { get; set; }
        public required string Email { get; set; }
        public GenderEnum Gender { get; set; }

        public UpdateUserCommand(Guid id, string name, string email, GenderEnum gender = GenderEnum.RatherNotSay)
        {
            Id = id;
            Name = name;
            Email = email;
            Gender = gender;
        }

        public override bool IsValid()
        {
            ValidationResult = new UpdateUserCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}