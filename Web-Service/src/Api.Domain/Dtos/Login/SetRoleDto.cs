using System.ComponentModel.DataAnnotations;

namespace Api.Domain.Dtos.Login
{
    public class SetRoleDto
    {
        [Required(ErrorMessage = "User ID is required")]
        public Guid UserId { get; set; }

        [Required (ErrorMessage = "Role is required")]        
        public string NewRole { get; set; } = string.Empty;
    }
}