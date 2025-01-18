using CA_Jason_Taylor_template.Application.Common.Mappings;
using CA_Jason_Taylor_template.Domain.Entities;

namespace CA_Jason_Taylor_template.Application.Common.Models;

// Note: This is currently just used to demonstrate applying multiple IMapFrom attributes.
public class LookupDto : IMapFrom<TodoList>, IMapFrom<TodoItem>
{
    public int Id { get; init; }

    public string? Title { get; init; }
}
