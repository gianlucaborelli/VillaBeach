using MediatR;

namespace Api.Domain.Events.User
{
    public class UserEventHandler :
    INotificationHandler<CreatedNewUserEvent>,
    INotificationHandler<UpdatedUserEvent>,
    INotificationHandler<DeletedUserEvent>,
    INotificationHandler<UpdatedAddressToUserEvent>,
    INotificationHandler<AddedAddressToUserEvent>
    {
        public Task Handle(CreatedNewUserEvent notification, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public Task Handle(UpdatedUserEvent notification, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public Task Handle(DeletedUserEvent notification, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public Task Handle(UpdatedAddressToUserEvent notification, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public Task Handle(AddedAddressToUserEvent notification, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}