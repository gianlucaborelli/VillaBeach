using Api.Service.Interfaces;
using Api.Domain.Dtos.User;

namespace Api.Service.Services
{
    public class UserService : IUserService
    {
        public Task Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Exists(string email)
        {
            throw new NotImplementedException();
        }

        public Task<UserDto?> FindByEmail(string email)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<UserDto>?> FindByName(string name)
        {
            throw new NotImplementedException();
        }

        public Task<UserDto> Get(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<UserDto>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<UserDtoCreateResult> Post(UserDtoCreate user)
        {
            throw new NotImplementedException();
        }

        public Task<UserDtoUpdateResult> Put(UserDtoUpdateRequest user)
        {
            throw new NotImplementedException();
        }
    }
}