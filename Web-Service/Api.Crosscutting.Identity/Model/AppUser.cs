using Api.Core.Domain;
using Microsoft.AspNetCore.Identity;

namespace Api.CrossCutting.Identity.Authentication.Model
{
    public class AppUser : IdentityEntity
    {
        public required string Name { get; set; }
    }
}