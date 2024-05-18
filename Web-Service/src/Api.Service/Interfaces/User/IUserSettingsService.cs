namespace Api.Service.Interfaces
{
    public interface IUserSettingsService
    {
        Dictionary<string, int> GetSettingByUserId();

        Task<bool> UpdateSetting(string key, int value);       
        
    }
}