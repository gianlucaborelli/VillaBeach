namespace Api.Domain.Dtos.Login
{
    public class RefreshTokenDto
    {        
        public string Token { get; set; } = string.Empty;
        public DateTime? Expires { get; set; }
        public DateTime Created { get; set; }
    }
}