using FluentValidation;

namespace Api.Domain.Commands.AuthenticationCommands.Validations
{
    public class ForgetPasswordRequestCommandValidation: AbstractValidator<ForgetPasswordRequestCommand>
    {
        public ForgetPasswordRequestCommandValidation()
        {
            RuleFor(u => u.Email)
                .NotEmpty().WithMessage("Please ensure you have entered the Email");
        }
    }
}