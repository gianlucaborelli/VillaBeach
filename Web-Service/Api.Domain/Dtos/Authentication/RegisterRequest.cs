using System.ComponentModel.DataAnnotations;
using Swashbuckle.AspNetCore.Annotations;

namespace Api.Domain.Dtos.Authentication
{    
    public class RegisterRequest
    {        
        [Required(ErrorMessage = "Name is required")]
        [SwaggerSchema(Description = "Full name of the user.")]
        public required string Name { get; set; }
                
        [Required(ErrorMessage = "Email is required"), EmailAddress(ErrorMessage = "Invalid Email format")]
        [SwaggerSchema(Description = "Email of the user.")]
        public required string Email { get; set; } 
                
        [Required(ErrorMessage = "Password is required"), MinLength(6, ErrorMessage = "Password must have a minimum of 8 characters.")]
        [SwaggerSchema(Description = "Password of the user.")]        
        public required string Password { get; set; }
        
        [Compare("Password")]
        [SwaggerSchema(Description = "Confirm password of the user.")]
        public required string ConfirmPassword { get; set; }
    }
}