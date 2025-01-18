using CA_Jason_Taylor_template.Application.Common.Mappings;
using CA_Jason_Taylor_template.Domain.Entities;

namespace CA_Jason_Taylor_template.Application.TodoLists.Queries.ExportTodos;

public class TodoItemRecord : IMapFrom<TodoItem>
{
    public string? Title { get; init; }

    public bool Done { get; init; }
}
