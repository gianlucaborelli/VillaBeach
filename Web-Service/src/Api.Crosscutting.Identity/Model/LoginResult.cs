using FluentValidation.Results;

namespace Api.CrossCutting.Identity.Authentication.Model
{
    public class LoginResult
    {            
        public string AccessToken { get; set; } = string.Empty;

        public string RefreshToken { get; set; } = string.Empty;

        public required ValidationResult ValidationResult { get; set; }
    }
}