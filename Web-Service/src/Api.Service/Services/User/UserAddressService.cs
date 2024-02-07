using Api.Domain.Dtos.User;
using Api.Domain.Entities;
using Api.Domain.Interfaces.Services.Authentication;
using Api.Domain.Interfaces.Services.User;
using Api.Domain.Models;
using Api.Domain.Repository;
using AutoMapper;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Api.Service.Services
{
    public class UserAddressService : IUserAddressService
    {
        private IUserRepository _repository;
        private IAuthenticationService _auth;
        private readonly IMapper _mapper;

        public UserAddressService(IUserRepository repository, IMapper mapper, IAuthenticationService auth)
        {
            _repository = repository;
            _mapper = mapper;
            _auth = auth;
        }

        public async Task<AddressModel> Get(Guid id)
        {
            var user = await _repository.FindById(_auth.GetUserId());
            var address = user!.AddressList!.FirstOrDefault(x => x.Id == id) ?? throw new InvalidOperationException("Address not found.");

            return _mapper.Map<AddressModel>(address); ;
        }

        public async Task<bool> Post(UserAddressDtoCreateRequest address)
        {
            var user = await _repository.FindById(_auth.GetUserId()) ?? throw new Exception("User not found.");

            user.AddressList.Add(_mapper.Map<AddressEntity>(address));

            await _repository.UpdateAsync(user);

            return true;
        }

        public Task<UserAddressDtoUpdateResult> Put(UserAddressDtoUpdateRequest user)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Delete(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}