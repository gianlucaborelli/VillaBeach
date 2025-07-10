using Api.Core.Events.Messaging;
using Api.Domain.Entities.UserEntityEnum;
using FluentValidation;

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

        public class UpdateUserCommandValidation : AbstractValidator<UpdateUserCommand>
        {

            public UpdateUserCommandValidation()
            {
                RuleFor(u => u.Id)
                    .NotEmpty().WithMessage("Please ensure you have entered the Id")
                    .NotEqual(Guid.Empty).WithMessage("Please ensure you have entered the Id");

                RuleFor(u => u.Name)
                    .NotEmpty().WithMessage("Please ensure you have entered the Name")
                    .Length(3, 150).WithMessage("The Name must have between 3 and 150 characters");

                RuleFor(u => u.Email)
                    .EmailAddress()
                    .NotEmpty().WithMessage("Please ensure you have entered the E-mail")
                    .Length(3, 150).WithMessage("The email must have between 3 and 150 characters");

                RuleFor(u => u.Gender)
                    .IsInEnum().WithMessage("Please ensure you have entered a Gender valid value");
            }
        }
    }
}