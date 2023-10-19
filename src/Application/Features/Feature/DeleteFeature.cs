using MediatR;

using Microsoft.AspNetCore.Mvc;

using Sirus.Application.Common;
using Sirus.Application.Common.Exceptions;
using Sirus.Application.Infrastructure.Database;

namespace Sirus.Application.Features.Feature;

public class DeleteFeatureController : ApiControllerBase
{
    [HttpDelete("/api/features/{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        await Mediator.Send(new DeleteFeatureCommand { Id = id });

        return NoContent();
    }
}

public class DeleteFeatureCommand : IRequest
{
    public int Id { get; set; }
}

internal sealed class DeleteFeatureCommandHandler : IRequestHandler<DeleteFeatureCommand>
{
    private readonly FeatureDbContext _context;

    public DeleteFeatureCommandHandler(FeatureDbContext context)
    {
        _context = context;
    }

    async Task IRequestHandler<DeleteFeatureCommand>.Handle(DeleteFeatureCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Features
            .FindAsync(new object[] { request.Id }, cancellationToken) ?? throw new NotFoundException(nameof(Feature), request.Id);
       
        _context.Features.Remove(entity);

        entity.DomainEvents.Add(new FeatureDeletedEvent(entity));

        await _context.SaveChangesAsync(cancellationToken);
    }
}

public class FeatureDeletedEvent : DomainEvent
{
    public FeatureDeletedEvent(Entities.Feature item)
    {
        Item = item;
    }

    public Entities.Feature Item { get; }
}