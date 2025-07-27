using System.ComponentModel.DataAnnotations;

namespace Api.Domain.Dtos.User
{
    public class UserSettingsUpdateRequestDto
    {
        [Required]
        public string Key { get; set; } = string.Empty;
        [Required]
        public int Value { get; set; }
    }
}