using Api.Domain.Dtos.User;

namespace Api.Domain.Dtos.Login
{
    public class LoginDtoResult
    {            
        public string AccessToken { get; set; } = string.Empty;

        public string RefreshToken { get; set; } = string.Empty;

        public required UserSettingsDto Settings {get; set;} 
    }
}