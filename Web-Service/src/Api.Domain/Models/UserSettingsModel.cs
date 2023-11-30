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

        private Guid _userId;
        public Guid UserId
        {
            get { return _userId; }
            set { _userId = value; }
        }       
    }
}