using Api.Service.Interfaces;
using Api.Domain.Dtos.User;
using FluentValidation.Results;
using Api.Domain.Interface;
using AutoMapper;
using Api.Core.Mediator;
using Api.Domain.Commands.UserCommands;

namespace Api.Service.Services
{
    public class UserService(
        IUserRepository userRepository,
        IMediatorHandler mediator,
        IMapper mapper) : IUserService
    {
        private readonly IUserRepository _userRepository = userRepository;
        private readonly IMediatorHandler _mediator = mediator;
        private readonly IMapper _mapper = mapper;

        public async Task<bool> Exist(Guid id)
        {
            return await _userRepository.ExistAsync(id);
        }

        public async Task<List<UserView>> GetAll()
        {
            return _mapper.Map<List<UserView>>(await _userRepository.GetAllAsync());
        }

        public async Task<UserView> GetById(Guid id)
        {
            return _mapper.Map<UserView>(await _userRepository.GetByIdAsync(id));
        }

        public async Task<IEnumerable<UserView>?> GetByName(string name)
        {
            return _mapper.Map<List<UserView>>(await _userRepository.GetByNameAsync(name));
        }

        public Task<ValidationResult> CreateUser(CreateUserRequest user)
        {
            var command = _mapper.Map<CreateNewUserCommand>(user);
            return _mediator.SendCommand(command);
        }

        public Task<ValidationResult> UpdateUser(UpdateUserRequest user)
        {
            var command = _mapper.Map<UpdateUserCommand>(user);
            return _mediator.SendCommand(command);
        }

        public Task<ValidationResult> DeleteUser(Guid id)
        {
            var command = new DeleteUserCommand(id);
            return _mediator.SendCommand(command);
        }

        public Task<ValidationResult> AddAddress(AddAddressRequest address, Guid userId)
        {
            var command = _mapper.Map<AddAddressToUserCommand>(address);
            command.UserId = userId;
            return _mediator.SendCommand(command);
        }

        public Task<ValidationResult> AddPhone(AddPhoneRequest phone)
        {            
            throw new NotImplementedException();
        }

        public Task<ValidationResult> UpdateAddress(UpdateAddressRequest address, Guid userId)
        {
            var command = _mapper.Map<UpdateUserAddressCommand>(address);
            command.UserId = userId;
            return _mediator.SendCommand(command);
        }

        public Task<ValidationResult> UpdatePhone(UpdatePhoneRequest phone)
        {
            throw new NotImplementedException();
        }

        public Task<ValidationResult> DeleteAddress(Guid addressId)
        {
            throw new NotImplementedException();
        }

        public Task<ValidationResult> DeletePhone(Guid phoneId)
        {
            throw new NotImplementedException();
        }
    }
}