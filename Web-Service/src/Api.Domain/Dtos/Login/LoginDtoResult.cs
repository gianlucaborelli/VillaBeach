namespace Api.Domain.Dtos.Login
{
    public class LoginDtoResult
    {            
        public string AccessToken { get; set; } = string.Empty;

        public string RefreshToken { get; set; } = string.Empty;
    }
}