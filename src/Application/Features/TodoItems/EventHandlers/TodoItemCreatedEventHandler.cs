using MediatR;

using Microsoft.Extensions.Logging;

using Sirus.Application.Common.Models;
using Sirus.Application.Features.Feature.EventHandlers;

namespace Sirus.Application.Features.TodoItems.EventHandlers;

public class TodoItemCreatedEventHandler : INotificationHandler<DomainEventNotification<TodoItemCreatedEvent>>
{
    private readonly ILogger<FeatureCreatedEventHandler> _logger;

    public TodoItemCreatedEventHandler(ILogger<FeatureCreatedEventHandler> logger)
    {
        _logger = logger;
    }

    public Task Handle(DomainEventNotification<TodoItemCreatedEvent> notification, CancellationToken cancellationToken)
    {
        var domainEvent = notification.DomainEvent;

        _logger.LogInformation("VerticalSlice Domain Event: {DomainEvent}", domainEvent.GetType().Name);

        return Task.CompletedTask;
    }
}
