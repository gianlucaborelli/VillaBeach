using System.ComponentModel.DataAnnotations;

namespace Api.Domain.Dtos.Authentication
{
    public class ChangePasswordRequest
    {
        [Required (ErrorMessage = "Password is required"), MinLength(6, ErrorMessage ="The Current Password must have a minimum of 8 characters.")]
        public required string CurrentPassword { get; set; }

        [Required (ErrorMessage = "New Password is required"), MinLength(6, ErrorMessage ="The New Password must have a minimum of 8 characters.")]
        public required string NewPassword { get; set; } 

        [Compare("NewPassword")]
        public required string NewPasswordConfirm { get; set; } 
    }
}