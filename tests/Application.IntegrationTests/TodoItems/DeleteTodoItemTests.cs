using FluentAssertions;

using NUnit.Framework;

using Sirus.Application.Common.Exceptions;
using Sirus.Application.Entities;
using Sirus.Application.Features.TodoItems;
using Sirus.Application.Features.TodoLists;

using static Sirus.Application.IntegrationTests.Testing;

namespace Sirus.Application.IntegrationTests.TodoItems;
public class DeleteTodoItemTests : TestBase
{
    [Test]
    public async Task ShouldRequireValidTodoItemId()
    {
        var command = new DeleteTodoItemCommand { Id = 99 };

        await FluentActions.Invoking(() =>
            SendAsync(command)).Should().ThrowAsync<NotFoundException>();
    }

    [Test]
    public async Task ShouldDeleteTodoItem()
    {
        var listId = await SendAsync(new CreateTodoListCommand
        {
            Title = "New List"
        });

        var itemId = await SendAsync(new CreateTodoItemCommand
        {
            ListId = listId,
            Title = "New Item"
        });

        await SendAsync(new DeleteTodoItemCommand
        {
            Id = itemId
        });

        var item = await FindAsync<TodoItem>(itemId);

        item.Should().BeNull();
    }
}
