using Api.Domain.Dtos.User;
using Api.Domain.Entities;

namespace Api.Service.Interfaces
{
    public interface IUserAddressService
    {
        Task<Address> Get (Guid id);          

        Task<bool> Post (UserAddressDtoCreateRequest address);        

        Task<UserAddressDtoUpdateResult> Put (UserAddressDtoUpdateRequest address);

        Task<bool> Delete (Guid id);
    }
}