using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Api.Domain.Entities.UserEntityEnum;

namespace Api.Domain.Dtos.User
{
    public class UserDtoCreate
    {
        public string? Name { get; set; }
        [EmailAddress]
        public string? Email { get; set; }
        public GenderEnum Gender { get; set; } = GenderEnum.RatherNotSay;
    }
}