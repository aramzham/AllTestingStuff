using CA_Jason_Taylor_template.Application.TodoLists.Queries.ExportTodos;

namespace CA_Jason_Taylor_template.Application.Common.Interfaces;

public interface ICsvFileBuilder
{
    byte[] BuildTodoItemsFile(IEnumerable<TodoItemRecord> records);
}
