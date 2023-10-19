using FluentValidation;

using MediatR;

using Microsoft.AspNetCore.Mvc;

using Sirus.Application.Common;
using Sirus.Application.Infrastructure.Database;

namespace Sirus.Application.Features.Feature;

public class CreateFeatureController : ApiControllerBase
{
    [HttpPost("/api/features")]
    public async Task<ActionResult<int>> Create(CreateFeatureCommand command)
    {
        return await Mediator.Send(command);
    }
}

public class CreateFeatureCommand : IRequest<int>
{
    // add properties here
}

public class CreateFeatureCommandValidator : AbstractValidator<CreateFeatureCommand>
{
    public CreateFeatureCommandValidator()
    {
        // add rules here

        //RuleFor(v => v.Title)
        //    .MaximumLength(200)
        //    .NotEmpty();
    }
}

internal sealed class CreateFeatureCommandHandler : IRequestHandler<CreateFeatureCommand, int>
{
    private readonly FeatureDbContext _context;

    public CreateFeatureCommandHandler(FeatureDbContext context)
    {
        _context = context;
    }

    public async Task<int> Handle(CreateFeatureCommand request, CancellationToken cancellationToken)
    {
        var entity = new Entities.Feature
        {
            // add properties here
            Done = false
        };

        entity.DomainEvents.Add(new FeatureCreatedEvent(entity));

        _context.Features.Add(entity);

        await _context.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }
}

public class FeatureCreatedEvent : DomainEvent
{
    public FeatureCreatedEvent(Entities.Feature item)
    {
        Item = item;
    }

    public Entities.Feature Item { get; }
}