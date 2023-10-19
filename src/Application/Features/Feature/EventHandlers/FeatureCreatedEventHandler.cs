using MediatR;

using Microsoft.Extensions.Logging;

using Sirus.Application.Common.Models;

namespace Sirus.Application.Features.Feature.EventHandlers;

public class FeatureCreatedEventHandler : INotificationHandler<DomainEventNotification<FeatureCreatedEvent>>
{
    private readonly ILogger<FeatureCreatedEventHandler> _logger;

    public FeatureCreatedEventHandler(ILogger<FeatureCreatedEventHandler> logger)
    {
        _logger = logger;
    }

    public Task Handle(DomainEventNotification<FeatureCreatedEvent> notification, CancellationToken cancellationToken)
    {
        var domainEvent = notification.DomainEvent;

        _logger.LogInformation("VerticalSlice Domain Event: {DomainEvent}", domainEvent.GetType().Name);

        return Task.CompletedTask;
    }
}
