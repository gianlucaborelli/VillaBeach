using Api.CrossCutting.Communication.Interfaces;
using MediatR;

namespace Api.Domain.Events
{
    class AuthenticationEventHandler :
        INotificationHandler<NewUserRegisteredEvent>,
        INotificationHandler<UserLoggedInEvent>,
        INotificationHandler<ForgottenPasswordRecoveryEvent>
    {
        private readonly IEmailService _emailService;

        public AuthenticationEventHandler(IEmailService emailService)
        {
            _emailService = emailService;
        }

        public Task Handle(NewUserRegisteredEvent notification, CancellationToken cancellationToken)
        {
            _emailService.SendEmailVerification(notification.Email, notification.Name, notification.TokenUrl);
            return Task.CompletedTask;
        }

        public Task Handle(UserLoggedInEvent notification, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public Task Handle(ForgottenPasswordRecoveryEvent notification, CancellationToken cancellationToken)
        {
            _emailService.SendForgotPasswordEmail(notification.Email, notification.Name, notification.RecoveryToken);
            return Task.CompletedTask;
        }
    }
}