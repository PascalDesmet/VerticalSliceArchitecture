﻿using System.Globalization;

using CsvHelper.Configuration;

using Sirus.Application.Features.TodoLists;

namespace Sirus.Application.Infrastructure.Files.Maps;

public class TodoItemRecordMap : ClassMap<TodoItemRecord>
{
    public TodoItemRecordMap()
    {
        AutoMap(CultureInfo.InvariantCulture);

        Map(m => m.Done).ConvertUsing(c => c.Done ? "Yes" : "No");
    }
}
