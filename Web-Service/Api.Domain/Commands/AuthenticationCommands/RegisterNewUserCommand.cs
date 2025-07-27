using Api.Core.Events.Messaging;
using FluentValidation;

namespace Api.Domain.Commands.AuthenticationCommands
{
    public class RegisterNewUserCommand : Command
    {
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string ConfirmPassword { get; set; } = string.Empty;

        public RegisterNewUserCommand() { }
        public RegisterNewUserCommand(string name, string email, string password, string confirmPassword)
        {
            Name = name;
            Email = email;
            Password = password;
            ConfirmPassword = confirmPassword;
        }

        public override bool IsValid()
        {
            ValidationResult = new RegisterNewUserCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }

        public class RegisterNewUserCommandValidation : AbstractValidator<RegisterNewUserCommand>
        {
            public RegisterNewUserCommandValidation()
            {
                RuleFor(u => u.Name)
                    .NotEmpty().WithMessage("Please ensure you have entered the Name")
                    .Length(2, 150).WithMessage("The Name must have between 2 and 150 characters");

                RuleFor(u => u.Email)
                    .EmailAddress()
                    .NotEmpty().WithMessage("Please ensure you have entered the E-mail")
                    .Length(2, 150).WithMessage("The email must have between 2 and 150 characters");

                RuleFor(x => x.Password)
                    .NotEmpty().WithMessage("Password is required.")
                    .MinimumLength(6).WithMessage("Password must be at least 6 characters long.");

                RuleFor(x => x.ConfirmPassword)
                    .Equal(x => x.Password).WithMessage("Password confirmation must match the password.");
            }
        }
    }
}

