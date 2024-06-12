using Api.Core.Events;
using Api.Core.Events.Messaging;
using Api.Core.Mediator;
using FluentValidation.Results;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Api.CrossCutting.Bus;

public sealed class InMemoryBus : IMediatorHandler
{
    private readonly IMediator _mediator;
    private readonly IEventStore _eventStore;
    private readonly ILogger<InMemoryBus> _logger;

    public InMemoryBus(IEventStore eventStore, IMediator mediator, ILogger<InMemoryBus> logger)
    {
        _eventStore = eventStore;
        _mediator = mediator;
        _logger = logger;
    }

    public async Task PublishEvent<T>(T @event) where T : Event
    {
        if (@event == null)
            throw new ArgumentNullException(nameof(@event), "Event cannot be null");

        var eventType = @event.GetType().Name;

        try
        {
            if (!@event.MessageType.Equals("DomainNotification"))
                _eventStore?.Save(@event);

            await _mediator.Publish(@event);
            _logger.LogInformation($"Published event of type {eventType}");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error publishing event of type {eventType}");
            throw;
        }
    }

    public async Task<ValidationResult> SendCommand<T>(T command) where T : Command
    {
        try
        {
            return await _mediator.Send(command);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error sending command of type {typeof(T).Name}");
            throw;
        }
    }

    Task<ValidationResult> IMediatorHandler.SendCommand<T>(T command)
    {
        throw new NotImplementedException();
    }
}