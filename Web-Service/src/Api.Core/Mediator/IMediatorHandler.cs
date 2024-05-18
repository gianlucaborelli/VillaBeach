using Api.Core.Events.Messaging;
using FluentValidation.Results;

namespace Api.Core.Mediator;

public interface IMediatorHandler
{
    Task PublishEvent<T>(T @event) where T : Event;
    Task<ValidationResult> SendCommand<T>(T command) where T : Command;
}
