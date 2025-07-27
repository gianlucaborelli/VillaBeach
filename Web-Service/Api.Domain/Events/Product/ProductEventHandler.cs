using MediatR;

namespace Api.Domain.Events.Product
{
    public class ProductEventHandler :
        INotificationHandler<CreatedProductEvent>,
        INotificationHandler<UpdatedProductEvent>,
        INotificationHandler<DeletedProductEvent>
    {
        public Task Handle(CreatedProductEvent notification, CancellationToken cancellationToken)
        {
            // Aqui você pode adicionar lógica adicional quando um produto for criado
            // Por exemplo, enviar notificações, logs, etc.
            return Task.CompletedTask;
        }

        public Task Handle(UpdatedProductEvent notification, CancellationToken cancellationToken)
        {
            // Aqui você pode adicionar lógica adicional quando um produto for atualizado
            // Por exemplo, enviar notificações, logs, etc.
            return Task.CompletedTask;
        }

        public Task Handle(DeletedProductEvent notification, CancellationToken cancellationToken)
        {
            // Aqui você pode adicionar lógica adicional quando um produto for excluído
            // Por exemplo, enviar notificações, logs, etc.
            return Task.CompletedTask;
        }
    }
} 