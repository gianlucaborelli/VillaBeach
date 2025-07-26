using Api.Core.Events.Messaging;
using System;

namespace Api.Domain.Events.Purchase
{
    public class DeletedPurchaseEvent : DomainEvent
    {
        public DeletedPurchaseEvent(Guid id) : base(id)
        {
        }
    }
} 