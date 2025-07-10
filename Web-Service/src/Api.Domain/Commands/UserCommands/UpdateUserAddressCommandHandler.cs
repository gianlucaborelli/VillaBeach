using Api.Core.Events.Messaging;
using Api.Domain.Commands.UserCommands;
using Api.Domain.Events.User;
using Api.Domain.Interface;
using FluentValidation.Results;
using MediatR;

namespace Api.Domain.Commands.UserCommands
{
    public class UpdateUserAddressCommandHandler(
            IUserRepository userRepository) : CommandHandler,
        IRequestHandler<UpdateUserAddressCommand, ValidationResult>
    {
        private readonly IUserRepository _userRepository = userRepository;

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
