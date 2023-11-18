namespace Api.Domain.Dtos.Login
{
    public class RefreshTokenDtoResult
    {            
        public string AccessToken { get; set; } = string.Empty;

        public string RefreshToken { get; set; } = string.Empty;
    }
}