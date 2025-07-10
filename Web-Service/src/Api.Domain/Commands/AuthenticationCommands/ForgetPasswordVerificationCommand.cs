using Api.Core.Events.Messaging;
using FluentValidation;

namespace Api.Domain.Commands.AuthenticationCommands
{
    public class ForgetPasswordVerificationCommand : Command
    {
        public required string Token { get; set; }

        public required string Email { get; set; }

        public required string Password { get; set; }

        public required string ConfirmPassword { get; set; }

        public ForgetPasswordVerificationCommand(string token, string email, string password, string confirmPassword)
        {            
            Token = token;
            Email = email;
            Password = password;
            ConfirmPassword = confirmPassword;
        }

        public override bool IsValid()
        {
            ValidationResult = new ForgetPasswordVerificationCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }

        public class ForgetPasswordVerificationCommandValidation : AbstractValidator<ForgetPasswordVerificationCommand>
        {
            public ForgetPasswordVerificationCommandValidation()
            {
                RuleFor(x => x.Token)
                    .NotEmpty().WithMessage("Please ensure you have entered the Token");

                RuleFor(x => x.Email)
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