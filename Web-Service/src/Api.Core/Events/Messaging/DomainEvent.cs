﻿namespace Api.Core.Events.Messaging;

public abstract class DomainEvent : Event
{
    protected DomainEvent(Guid aggregateId)
    {
        AggregateId = aggregateId;
    }
}