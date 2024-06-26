using System.ComponentModel.DataAnnotations;

namespace Api.Domain.Dtos.Authentication
{
    public class LoginDtoRequest
    {
        [Required (ErrorMessage = "Email is required"), EmailAddress(ErrorMessage = "Invalid Email format")]
        public string Email { get; set; } = string.Empty;

        [Required (ErrorMessage = "Password is required"), MinLength(6, ErrorMessage ="Password must have a minimum of 8 characters.")]
        public string Password { get; set; } = string.Empty;
    }
}