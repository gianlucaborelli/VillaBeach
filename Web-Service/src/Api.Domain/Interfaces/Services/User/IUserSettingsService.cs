
namespace Api.Domain.Interfaces.Services.User
{
    public interface IUserSettingsService
    {
        Dictionary<string, int> GetSettingByUserId();

        bool UpdateSetting(int userId, string key, int value);       
        
    }
}