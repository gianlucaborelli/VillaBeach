using System.ComponentModel.DataAnnotations;
using Api.Domain.Entities.UserEntityEnum;

namespace Api.Domain.Dtos.User
{
    public class CreateUserRequest
    {   
        [Required (ErrorMessage = "Name is required")]
        [MinLength(3, ErrorMessage = "Name must have at least 3 characters")]
        [MaxLength(150, ErrorMessage = "Name must have a maximum of 150 characters")]
        public string? Name { get; set; }

        [Required (ErrorMessage = "Email is required")]
        [EmailAddress (ErrorMessage = "Email is invalid")]
        [MinLength(3, ErrorMessage = "Email must have at least 5 characters")]
        [MaxLength(150, ErrorMessage = "Email must have a maximum of 150 characters")]
        public string? Email { get; set; }
    }
}