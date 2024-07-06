
using FluentValidation;

namespace Api.Domain.Commands.UserCommands.Validations
{
    public class DeleteUserCommandValidation: AbstractValidator<DeleteUserCommand>
    {
        public DeleteUserCommandValidation()
        {
            RuleFor(u => u.Id)
                .NotEqual(Guid.Empty);
        }        
    }
}