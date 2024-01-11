using System.ComponentModel.DataAnnotations;
using Api.Domain.Entities.UserEntityEnum;

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