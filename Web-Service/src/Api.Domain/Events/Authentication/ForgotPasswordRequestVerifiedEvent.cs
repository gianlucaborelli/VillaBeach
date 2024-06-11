using Api.Core.Events.Messaging;

namespace Api.Domain.Events.Authentication
{
    public class ForgotPasswordRequestVerifiedEvent : Event
    {
        public Guid Id { get; set; }
        
        public string Email { get; private set; }

        public bool Failed { get; set; }

        public ForgotPasswordRequestVerifiedEvent(Guid id, string email, bool failed)
        {
            Id = id;
            Email = email;
            Failed = failed;
        }
    }
}