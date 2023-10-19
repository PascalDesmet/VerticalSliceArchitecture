using FluentValidation;

using MediatR;

using Microsoft.AspNetCore.Mvc;

using Sirus.Application.Common;
using Sirus.Application.Common.Exceptions;
using Sirus.Application.Infrastructure.Database;

namespace Sirus.Application.Features.Feature;

public class FeaturesController : ApiControllerBase
{
    [HttpPut("/api/features/{id}")]
    public async Task<ActionResult> Update(int id, UpdateFeatureCommand command)
    {
        if (id != command.Id)
        {
            return BadRequest();
        }

        await Mediator.Send(command);

        return NoContent();
    }
}

public class UpdateFeatureCommand : IRequest
{
    public int Id { get; set; }

   // add properties here

    public bool Done { get; set; }
}

public class UpdateFeatureCommandValidator : AbstractValidator<UpdateFeatureCommand>
{
    public UpdateFeatureCommandValidator()
    {
        // add rules here

        //RuleFor(v => v.Title)
        //    .MaximumLength(200)
        //    .NotEmpty();
    }
}

internal sealed class UpdateFeatureCommandHandler : IRequestHandler<UpdateFeatureCommand>
{
    private readonly FeatureDbContext _context;

    public UpdateFeatureCommandHandler(FeatureDbContext context)
    {
        _context = context;
    }

    async Task IRequestHandler<UpdateFeatureCommand>.Handle(UpdateFeatureCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Features
            .FindAsync(new object[] { request.Id }, cancellationToken) ?? throw new NotFoundException(nameof(Feature), request.Id);
        
        // update properties here
        entity.Done = request.Done;

        await _context.SaveChangesAsync(cancellationToken);
    }
}