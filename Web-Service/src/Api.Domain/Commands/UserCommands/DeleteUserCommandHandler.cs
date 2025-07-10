using Api.Core.Events.Messaging;
using Api.Domain.Commands.UserCommands;
using Api.Domain.Events.User;
using Api.Domain.Interface;
using FluentValidation.Results;
using MediatR;

namespace Api.Domain.Commands.UserCommands
{
    public class DeleteUserCommandHandler(
            IUserRepository userRepository) : CommandHandler,
        IRequestHandler<DeleteUserCommand, ValidationResult>
    {
        private readonly IUserRepository _userRepository = userRepository;

        public async Task<ValidationResult> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid()) return request.ValidationResult;

            var user = await _userRepository.GetByIdAsync(request.Id);

            if (user is null)
            {
                AddError("The user does not exist.");
                return ValidationResult;
            }

            _userRepository.Delete(user.Id);

            user.AddDomainEvent(new DeletedUserEvent(
                user.Id,
                user.Name,
                user.Email
                ));

            return await Commit(_userRepository.UnitOfWork);
        }
    }
}
