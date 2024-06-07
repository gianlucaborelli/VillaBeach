using System.ComponentModel.DataAnnotations;

namespace Api.CrossCutting.Identity.Authentication.Model
{
    public class EmailConfirmationRequest
    {   
        [Required (ErrorMessage = "Email is required"), EmailAddress(ErrorMessage = "Invalid Email format")]
        public required string Email { get; set; }
        [Required (ErrorMessage = "Token is required")]
        public required string Token { get; set; }
    }
}