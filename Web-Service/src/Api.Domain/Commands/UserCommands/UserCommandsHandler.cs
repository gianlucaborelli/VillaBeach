using Api.Core.Events.Messaging;
using Api.Domain.Entities;
using Api.Domain.Events.User;
using Api.Domain.Interface;
using FluentValidation.Results;
using MediatR;

namespace Api.Domain.Commands.UserCommands
{
    class UserCommandsHandler(
            IUserRepository _userRepository) : CommandHandler,
        IRequestHandler<CreateNewUserCommand, ValidationResult>,
        IRequestHandler<UpdateUserCommand, ValidationResult>,
        IRequestHandler<DeleteUserCommand, ValidationResult>,
        IRequestHandler<AddAddressToUserCommand, ValidationResult>,
        IRequestHandler<UpdateUserAddressCommand, ValidationResult>
    {
        private readonly IUserRepository _userRepository = _userRepository;

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

        public async Task<ValidationResult> Handle(UpdateUserAddressCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid()) return request.ValidationResult;

            var user = await _userRepository.GetByIdAsync(request.UserId);

            if (user is null)
            {
                AddError("The user does not exist.");
                return ValidationResult;
            }

            var address = user.AddressList!.FirstOrDefault(a => a.Id == request.Id);

            if (address is null)
            {
                AddError("The address does not exist.");
                return ValidationResult;
            }

            address.PostalCode = request.PostalCode;
            address.Street = request.Street;
            address.Number = request.Number;
            address.District = request.District;
            address.City = request.City;
            address.State = request.State;
            address.Description = request.Description;

            _userRepository.Update(user);
            
            user.AddDomainEvent(new UpdatedAddressToUserEvent(
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