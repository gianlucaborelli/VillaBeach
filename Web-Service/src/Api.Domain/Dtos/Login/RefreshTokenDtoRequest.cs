using System.ComponentModel.DataAnnotations;

namespace Api.Domain.Dtos.Login
{
    public class RefreshTokenDtoRequest
    {
        [Required]
        public string AccessToken { get; set; } = string.Empty;

        [Required]
        public string RefreshToken { get; set; } = string.Empty;
    }
}