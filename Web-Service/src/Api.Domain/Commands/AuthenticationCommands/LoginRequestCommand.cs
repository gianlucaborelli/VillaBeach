using Api.Core.Events.Messaging;

namespace Api.Domain.Commands.AuthenticationCommands
{
    public class LoginRequestCommand : Command
    {
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;

        public LoginRequestCommand(string email, string password)
        {
            Email = email;
            Password = password;
        }

        public override bool IsValid()
        {
            ValidationResult = new LoginRequestCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
