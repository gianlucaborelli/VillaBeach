using FluentValidation;

namespace Api.Domain.Commands.UserCommands.Validations
{
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