using System.Globalization;
using CA_Jason_Taylor_template.Application.Common.Interfaces;
using CA_Jason_Taylor_template.Application.TodoLists.Queries.ExportTodos;
using CA_Jason_Taylor_template.Infrastructure.Files.Maps;
using CsvHelper;

namespace CA_Jason_Taylor_template.Infrastructure.Files;

public class CsvFileBuilder : ICsvFileBuilder
{
    public byte[] BuildTodoItemsFile(IEnumerable<TodoItemRecord> records)
    {
        using var memoryStream = new MemoryStream();
        using (var streamWriter = new StreamWriter(memoryStream))
        {
            using var csvWriter = new CsvWriter(streamWriter, CultureInfo.InvariantCulture);

            csvWriter.Context.RegisterClassMap<TodoItemRecordMap>();
            csvWriter.WriteRecords(records);
        }

        return memoryStream.ToArray();
    }
}
