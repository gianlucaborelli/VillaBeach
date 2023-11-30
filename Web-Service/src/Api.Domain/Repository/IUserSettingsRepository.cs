using Api.Domain.Entities;
using Api.Domain.Interfaces;

namespace Api.Domain.Repository
{
    public interface IUserSettingsRepository : IRepository<UserSettingsEntity>
    {
        Task<UserSettingsEntity?> GetSettingByUserId(Guid id);
    }
}