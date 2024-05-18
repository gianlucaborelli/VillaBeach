using Api.Domain.Entities.UserEntityEnum;

namespace Api.Domain.Dtos.User
{
    public class UserDtoUpdateResult
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set;}
        public GenderEnum Gender {get;set;} = GenderEnum.RatherNotSay;
    }
}