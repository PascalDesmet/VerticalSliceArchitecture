using MediatR;

using Microsoft.AspNetCore.Mvc;

using Sirus.Application.Common;
using Sirus.Application.Common.Exceptions;
using Sirus.Application.Entities;
using Sirus.Application.Infrastructure.Database;

namespace Sirus.Application.Features.TodoItems;

public class DeleteTodoItemController : ApiControllerBase
{
    [HttpDelete("/api/todo-items/{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        await Mediator.Send(new DeleteTodoItemCommand { Id = id });

        return NoContent();
    }
}

public class DeleteTodoItemCommand : IRequest
{
    public int Id { get; set; }
}

internal sealed class DeleteTodoItemCommandHandler : IRequestHandler<DeleteTodoItemCommand>
{
    private readonly ApplicationDbContext _context;

    public DeleteTodoItemCommandHandler(ApplicationDbContext context)
    {
        _context = context;
    }

    async Task IRequestHandler<DeleteTodoItemCommand>.Handle(DeleteTodoItemCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.TodoItems
            .FindAsync(new object[] { request.Id }, cancellationToken) ?? throw new NotFoundException(nameof(TodoItem), request.Id);
        _context.TodoItems.Remove(entity);

        entity.DomainEvents.Add(new TodoItemDeletedEvent(entity));

        await _context.SaveChangesAsync(cancellationToken);
    }
}

public class TodoItemDeletedEvent : DomainEvent
{
    public TodoItemDeletedEvent(TodoItem item)
    {
        Item = item;
    }

    public TodoItem Item { get; }
}