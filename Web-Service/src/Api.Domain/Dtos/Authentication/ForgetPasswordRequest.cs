using System.ComponentModel.DataAnnotations;

namespace Api.Domain.Dtos.Authentication
{
    public class ForgotPasswordRequest
    {
        [Required (ErrorMessage = "Email is required"), EmailAddress(ErrorMessage = "Invalid Email format")]
        public required string Email { get; set; }        
    }
}