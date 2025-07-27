using Api.Core.Events.Messaging;
using Api.Domain.Commands.UserCommands;
using Api.Domain.Entities;
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
    public class AddAddressToUserCommandHandler(
            IUserRepository userRepository) : CommandHandler,
        IRequestHandler<AddAddressToUserCommand, ValidationResult>
    {
        private readonly IUserRepository _userRepository = userRepository;

        public async Task<ValidationResult> Handle(AddAddressToUserCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid()) return request.ValidationResult;

            var user = await _userRepository.GetByIdAsync(request.UserId);

            if (user is null)
            {
                AddError("The user does not exist.");
                return ValidationResult;
            }

            var address = new Address
            {
                PostalCode = request.PostalCode,
                Street = request.Street,
                Number = request.Number,
                District = request.District,
                City = request.City,
                State = request.State,
                Description = request.Description
            };

            user.AddressList!.Add(address);

            _userRepository.Update(user);

            user.AddDomainEvent(new AddedAddressToUserEvent(
                user.Id,
                address.Id,
                address.PostalCode,
                address.Street,
                address.Number,
                address.District,
                address.City,
                address.State,
                address.Description!
            ));

            return await Commit(_userRepository.UnitOfWork);
        }
    }
}
