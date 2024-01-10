using Api.Domain.Entities;
using Api.Domain.Entities.UserEntityEnum;

namespace Api.Domain.Models
{
    public class UserSettingsModel : BaseModel
    {
        private ThemeModeEnum _themeMode;
        public ThemeModeEnum ThemeMode
        {
            get { return _themeMode; }
            set { _themeMode = value; }
        }                
    }
}