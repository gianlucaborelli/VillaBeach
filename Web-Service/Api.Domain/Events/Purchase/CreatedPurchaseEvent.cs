using Api.Core.Events.Messaging;
using System;

namespace Api.Domain.Events.Purchase
{
    public class CreatedPurchaseEvent : DomainEvent
    {
        public CreatedPurchaseEvent(Guid id) : base(id)
        {
        }
    }
} 