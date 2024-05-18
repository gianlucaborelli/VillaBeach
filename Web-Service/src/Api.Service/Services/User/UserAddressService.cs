using Api.Domain.Dtos.User;
using Api.Domain.Entities;
using Api.Service.Interfaces;

namespace Api.Service.Services
{
    public class UserAddressService : IUserAddressService
    {
        public Task<bool> Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<Address> Get(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Post(UserAddressDtoCreateRequest address)
        {
            throw new NotImplementedException();
        }

        public Task<UserAddressDtoUpdateResult> Put(UserAddressDtoUpdateRequest address)
        {
            throw new NotImplementedException();
        }
    }
}