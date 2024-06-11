using System.ComponentModel.DataAnnotations;

namespace Api.Domain.Dtos.Authentication
{
    public class ForgetPasswordVerificationRequest
    {
        [Required(ErrorMessage = "Token is required")]
        public required string Token { get; set; }

        [Required(ErrorMessage = "Email is required"), EmailAddress(ErrorMessage = "Invalid Email format")]
        public required string Email { get; set; }

        [Required(ErrorMessage = "Password is required"), MinLength(6, ErrorMessage = "Password must have a minimum of 8 characters.")]
        public required string Password { get; set; }

        [Compare("Password")]
        public required string ConfirmPassword { get; set; }
    }
}