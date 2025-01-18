namespace CA_Jason_Taylor_template.Application.TodoLists.Queries.GetTodos;

public class TodosVm
{
    public IReadOnlyCollection<PriorityLevelDto> PriorityLevels { get; init; } = Array.Empty<PriorityLevelDto>();

    public IReadOnlyCollection<TodoListDto> Lists { get; init; } = Array.Empty<TodoListDto>();
}
