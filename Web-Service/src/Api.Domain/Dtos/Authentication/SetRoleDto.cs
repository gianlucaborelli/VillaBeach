using System.ComponentModel.DataAnnotations;

namespace Api.Domain.Dtos.Authentication
{
    public class SetRoleDto
    {
        [Required(ErrorMessage = "Email is required"), EmailAddress(ErrorMessage = "Invalid Email format")]
        public required string UserEmail { get; set; }

        [Required(ErrorMessage = "Role is required")]
        public required string NewRole { get; set; }
    }
}