using FluentAssertions;

using NUnit.Framework;

using Sirus.Application.Common.Exceptions;
using Sirus.Application.Entities;
using Sirus.Application.Features.TodoLists;

using static Sirus.Application.IntegrationTests.Testing;

namespace Sirus.Application.IntegrationTests.TodoLists;
public class DeleteTodoListTests : TestBase
{
    [Test]
    public async Task ShouldRequireValidTodoListId()
    {
        var command = new DeleteTodoListCommand { Id = 99 };
        await FluentActions.Invoking(() => SendAsync(command)).Should().ThrowAsync<NotFoundException>();
    }

    [Test]
    public async Task ShouldDeleteTodoList()
    {
        var listId = await SendAsync(new CreateTodoListCommand
        {
            Title = "New List"
        });

        await SendAsync(new DeleteTodoListCommand
        {
            Id = listId
        });

        var list = await FindAsync<TodoList>(listId);

        list.Should().BeNull();
    }
}
