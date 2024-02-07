using Api.Domain.Dtos.User;
using Api.Domain.Models;

namespace Api.Domain.Interfaces.Services.User
{
    public interface IUserAddressService
    {
        Task<AddressModel> Get (Guid id);          

        Task<bool> Post (UserAddressDtoCreateRequest address);        

        Task<UserAddressDtoUpdateResult> Put (UserAddressDtoUpdateRequest address);

        Task<bool> Delete (Guid id);
    }
}