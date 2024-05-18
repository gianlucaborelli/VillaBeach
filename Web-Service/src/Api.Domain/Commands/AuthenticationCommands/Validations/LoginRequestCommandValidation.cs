using FluentValidation;

namespace Api.Domain.Commands.AuthenticationCommands
{
    public class LoginRequestCommandValidation : AbstractValidator<LoginRequestCommand>
    {
        public LoginRequestCommandValidation()
        {
            RuleFor(u => u.Email)
                .EmailAddress()
                .NotEmpty().WithMessage("Please ensure you have entered the E-mail")
                .Length(2, 150).WithMessage("The email must have between 2 and 150 characters");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password is required.")
                .MinimumLength(6).WithMessage("Password must be at least 6 characters long.");
        }
    }
}