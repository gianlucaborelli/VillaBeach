using FluentValidation;

namespace Api.Domain.Commands.AuthenticationCommands.Validations
{
    public class ForgetPasswordVerificationCommandValidation: AbstractValidator<ForgetPasswordVerificationCommand>
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