using MediatR;

using Microsoft.AspNetCore.Mvc;

using Sirus.Application.Common;
using Sirus.Application.Common.Exceptions;
using Sirus.Application.Entities;
using Sirus.Application.Infrastructure.Database;

namespace Sirus.Application.Features.Feature;

public class UpdateFeatureDetailController : ApiControllerBase
{
    [HttpPut("/api/features/[action]")]
    public async Task<ActionResult> UpdateItemDetails(int id, UpdateFeatureDetailCommand command)
    {
        if (id != command.Id)
        {
            return BadRequest();
        }

        await Mediator.Send(command);

        return NoContent();
    }
}

public class UpdateFeatureDetailCommand : IRequest
{
    public int Id { get; set; }

    // add properties here
}

internal sealed class UpdateFeatureDetailCommandHandler : IRequestHandler<UpdateFeatureDetailCommand>
{
    private readonly FeatureDbContext _context;

    public UpdateFeatureDetailCommandHandler(FeatureDbContext context)
    {
        _context = context;
    }

    async Task IRequestHandler<UpdateFeatureDetailCommand>.Handle(UpdateFeatureDetailCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Features
            .FindAsync(new object[] { request.Id }, cancellationToken) ?? throw new NotFoundException(nameof(Feature), request.Id);

        // update Feature detail properties here

        await _context.SaveChangesAsync(cancellationToken);
    }
}