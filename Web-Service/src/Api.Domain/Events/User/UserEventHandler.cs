using MediatR;

namespace Api.Domain.Events
{
    class UserEventHandler :
        INotificationHandler<NewUserRegisteredEvent>
    {
        public Task Handle(NewUserRegisteredEvent notification, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}