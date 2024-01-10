using Api.Domain.Entities.UserEntityEnum;

namespace Api.Domain.Entities
{
    public class UserSettingsEntity : BaseEntity
    {        
        public ThemeModeEnum ThemeMode {get; set;} = 0;        
    }
}