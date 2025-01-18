using CA_Jason_Taylor_template.Application.Common.Mappings;
using CA_Jason_Taylor_template.Domain.Entities;

namespace CA_Jason_Taylor_template.Application.TodoItems.Queries.GetTodoItemsWithPagination;

public class TodoItemBriefDto : IMapFrom<TodoItem>
{
    public int Id { get; init; }

    public int ListId { get; init; }

    public string? Title { get; init; }

    public bool Done { get; init; }
}
