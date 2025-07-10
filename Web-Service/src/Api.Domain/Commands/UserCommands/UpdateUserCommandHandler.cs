using Api.Core.Events.Messaging;
using Api.Domain.Commands.UserCommands;
using Api.Domain.Events.User;
using Api.Domain.Interface;
using FluentValidation.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Domain.Commands.UserCommands
{
    public class UpdateUserCommandHandler(
        IUserRepository userRepository) : CommandHandler,
        IRequestHandler<UpdateUserCommand, ValidationResult>
    {
        private readonly IUserRepository _userRepository = userRepository;

        public async Task<ValidationResult> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid()) return request.ValidationResult;

            var user = await _userRepository.GetByIdAsync(request.Id);

            if (user is null)
            {
                AddError("The user does not exist.");
                return ValidationResult;
            }

            user.Name = request.Name;
            user.Email = request.Email;
            user.Gender = request.Gender;

            _userRepository.Update(user);

            user.AddDomainEvent(new UpdatedUserEvent(
                user.Id,
                user.Name,
                user.Email,
                user.Gender));

            return await Commit(_userRepository.UnitOfWork);
        }
    }
}
