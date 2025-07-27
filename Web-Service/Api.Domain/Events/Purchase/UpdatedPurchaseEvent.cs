using Api.Core.Events.Messaging;
using System;

namespace Api.Domain.Events.Purchase
{
    public class UpdatedPurchaseEvent : DomainEvent
    {
        public UpdatedPurchaseEvent(Guid id) : base(id)
        {
        }
    }
} 