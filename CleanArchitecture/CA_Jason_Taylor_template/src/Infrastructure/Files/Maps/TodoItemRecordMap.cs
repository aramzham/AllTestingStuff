﻿using System.Globalization;
using CA_Jason_Taylor_template.Application.TodoLists.Queries.ExportTodos;
using CsvHelper.Configuration;

namespace CA_Jason_Taylor_template.Infrastructure.Files.Maps;

public class TodoItemRecordMap : ClassMap<TodoItemRecord>
{
    public TodoItemRecordMap()
    {
        AutoMap(CultureInfo.InvariantCulture);

        Map(m => m.Done).Convert(c => c.Value.Done ? "Yes" : "No");
    }
}
