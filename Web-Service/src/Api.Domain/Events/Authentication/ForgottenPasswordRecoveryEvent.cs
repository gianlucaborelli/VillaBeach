using Api.Core.Events.Messaging;

namespace Api.Domain.Events
{
    public class ForgottenPasswordRecoveryEvent : Event
    {
        public string Name { get; private set; }

        public string Email { get; private set; }

        public string RecoveryToken { get; private set;}

        public ForgottenPasswordRecoveryEvent(string name, string email, string recoveryToken)
        {
            Name = name;
            Email = email;
            RecoveryToken = recoveryToken;
        }        
    }
}