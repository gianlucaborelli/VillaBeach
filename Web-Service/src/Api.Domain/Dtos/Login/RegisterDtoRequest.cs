using System.ComponentModel.DataAnnotations;

namespace Api.Domain.Dtos.Login
{
    public class RegisterDtoRequest
    {
        [Required]
        public string Name { get; set; } = string.Empty;

        [Required (ErrorMessage = "Email is required"), EmailAddress(ErrorMessage = "Invalid Email format")]
        public string Email { get; set; } = string.Empty;

        [Required (ErrorMessage = "Password is required"), MinLength(8, ErrorMessage ="Password must have a minimum of 8 characters.")]
        public string Password { get; set; } = string.Empty;

        [Required, Compare("Password")]
        public string ConfirmPassword { get; set; } = string.Empty;
    }
}