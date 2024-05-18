using Api.Domain.Dtos.User;

namespace Api.Service.Interfaces
{
    public interface IUserService
    {
        Task<bool> Exists (string email);

        Task<UserDto> Get (Guid id);

        Task<IEnumerable<UserDto>> GetAll ();

        Task<UserDtoCreateResult> Post (UserDtoCreate user);

        Task<UserDtoUpdateResult> Put (UserDtoUpdateRequest user);

        Task Delete (Guid id);

        Task<IEnumerable<UserDto>?> FindByName (string name);

        Task<UserDto?> FindByEmail (string email);
    }
}