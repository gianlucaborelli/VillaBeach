using Api.Domain.Dtos.User;
using FluentValidation.Results;

namespace Api.Service.Interfaces
{
    public interface IUserService
    {
        Task<bool> Exist(Guid id);
        Task<UserView> GetById(Guid id);
        Task<IEnumerable<UserView>?> GetByName(string name);
        Task<List<UserView>> GetAll();
        Task<ValidationResult> CreateUser(CreateUserRequest user);
        Task<ValidationResult> UpdateUser(UpdateUserRequest user);
        Task<ValidationResult> DeleteUser(Guid id);
        Task<ValidationResult> AddAddress(AddAddressRequest address, Guid userId);
        Task<ValidationResult> UpdateAddress(UpdateAddressRequest address, Guid userId);
        Task<ValidationResult> DeleteAddress(Guid addressId);
        Task<ValidationResult> AddPhone(AddPhoneRequest phone);
        Task<ValidationResult> UpdatePhone(UpdatePhoneRequest phone);
        Task<ValidationResult> DeletePhone(Guid phoneId);
        // GetSettingByUserId
        // UpdateSetting
    }
}