﻿using MediatR;

using Microsoft.Extensions.Logging;

using Sirus.Application.Common.Models;
using Sirus.Application.Entities;

namespace Sirus.Application.Features.TodoItems.EventHandlers;

public class FeatureCompletedEventHandler : INotificationHandler<DomainEventNotification<TodoItemCompletedEvent>>
{
    private readonly ILogger<FeatureCompletedEventHandler> _logger;

    public FeatureCompletedEventHandler(ILogger<FeatureCompletedEventHandler> logger)
    {
        _logger = logger;
    }

    public Task Handle(DomainEventNotification<TodoItemCompletedEvent> notification, CancellationToken cancellationToken)
    {
        var domainEvent = notification.DomainEvent;

        _logger.LogInformation("VerticalSlice Domain Event: {DomainEvent}", domainEvent.GetType().Name);

        return Task.CompletedTask;
    }
}
