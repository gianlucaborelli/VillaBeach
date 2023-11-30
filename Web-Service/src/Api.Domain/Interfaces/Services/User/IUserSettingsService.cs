
namespace Api.Domain.Interfaces.Services.User
{
    public interface IUserSettingsService
    {
        Dictionary<string, int> GetSettingByUserId();

        Task<bool> UpdateSetting(string key, int value);       
        
    }
}