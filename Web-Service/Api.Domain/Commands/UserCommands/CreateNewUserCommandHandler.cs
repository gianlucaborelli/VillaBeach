using Api.Core.Events.Messaging;
using Api.Domain.Commands.UserCommands;
using Api.Domain.Entities;
using Api.Domain.Events.User;
using Api.Domain.Interface;
using FluentValidation.Results;
using MediatR;

namespace Api.Domain.Commands.UserCommands;

public class CreateNewUserCommandHandler(
    IUserRepository userRepository) : CommandHandler,
    IRequestHandler<CreateNewUserCommand, ValidationResult>
{
    private readonly IUserRepository _userRepository = userRepository;

    public async Task<ValidationResult> Handle(CreateNewUserCommand request, CancellationToken cancellationToken)
    {
        if (!request.IsValid()) return request.ValidationResult;

        var existingUser = await _userRepository.GetByEmailAsync(request.Email);

        if (existingUser is not null)
        {
            AddError("The user e-mail has already been taken.");
            return ValidationResult;
        }

        var user = new User
        {
            Id = Guid.NewGuid(),
            Name = request.Name,
            Email = request.Email
        };

        _userRepository.Add(user);

        user.AddDomainEvent(new CreatedNewUserEvent(
            user.Id,
            user.Name,
            user.Email
        ));

        return await Commit(_userRepository.UnitOfWork);
    }
}
