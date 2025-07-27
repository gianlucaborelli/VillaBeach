using Api.Core.Events.Messaging;

namespace Api.Domain.Events
{
    public class ForgottenPasswordRecoveryEvent : Event
    {
        public Guid Id { get; set; }
        public string Name { get; private set; }
        public string Email { get; private set; }
        public string RecoveryToken { get; private set;}

        public ForgottenPasswordRecoveryEvent(Guid id, string name, string email, string recoveryToken)
        {
            Id = id;
            Name = name;
            Email = email;
            RecoveryToken = recoveryToken;
        }        
    }
}