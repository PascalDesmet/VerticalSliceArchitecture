using FluentValidation;

using MediatR;

using Microsoft.AspNetCore.Mvc;

using VerticalSliceArchitecture.Application.Common;
using VerticalSliceArchitecture.Application.Entities;
using VerticalSliceArchitecture.Application.Infrastructure.Persistence;

namespace VerticalSliceArchitecture.Application.Features.ObjectInErrors;

public class CreateObjectInErrorController : ApiControllerBase
{
    [HttpPost("/api/object-in-errors")]
    public async Task<ActionResult<int>> Create(CreateObjectInErrorCommand command)
    {
        return await Mediator.Send(command);
    }
}

public class CreateObjectInErrorCommand : IRequest<int>
{
    public int Id { get; set; }

    public string? SomeValue { get; set; }
}

public class CreateObjectInErrorCommandValidator : AbstractValidator<CreateObjectInErrorCommand>
{
    public CreateObjectInErrorCommandValidator()
    {
        RuleFor(v => v.SomeValue)
            .MaximumLength(200)
            .NotEmpty();
    }
}

internal sealed class CreateObjectInErrorCommandHandler : IRequestHandler<CreateObjectInErrorCommand, int>
{
    private readonly ApplicationDbContext _context;

    public CreateObjectInErrorCommandHandler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<int> Handle(CreateObjectInErrorCommand request, CancellationToken cancellationToken)
    {
        var entity = new ObjectInError
        {
            Id = request.Id,
            SomeValue = request.SomeValue
        };

        _context.ObjectInErrors.Add(entity);

        await _context.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }
}