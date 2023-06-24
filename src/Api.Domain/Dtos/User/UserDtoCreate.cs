using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Domain.Dtos.User
{
    public class UserDtoCreate
    {
        public string? Name { get; set; }
        public string? Email { get; set;}
    }
}