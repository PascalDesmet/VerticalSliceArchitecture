﻿using MediatR;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using Sirus.Application.Common;
using Sirus.Application.Common.Exceptions;
using Sirus.Application.Entities;
using Sirus.Application.Infrastructure.Database;

namespace Sirus.Application.Features.TodoLists;

public class DeleteTodoListController : ApiControllerBase
{
    [HttpDelete("/api/todo-lists/{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        await Mediator.Send(new DeleteTodoListCommand { Id = id });

        return NoContent();
    }
}

public class DeleteTodoListCommand : IRequest
{
    public int Id { get; set; }
}

internal sealed class DeleteTodoListCommandHandler : IRequestHandler<DeleteTodoListCommand>
{
    private readonly ApplicationDbContext _context;

    public DeleteTodoListCommandHandler(ApplicationDbContext context)
    {
        _context = context;
    }

    async Task IRequestHandler<DeleteTodoListCommand>.Handle(DeleteTodoListCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.TodoLists
            .Where(l => l.Id == request.Id)
            .SingleOrDefaultAsync(cancellationToken) ?? throw new NotFoundException(nameof(TodoList), request.Id);
        _context.TodoLists.Remove(entity);

        await _context.SaveChangesAsync(cancellationToken);
    }
}
