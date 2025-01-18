using CA_Jason_Taylor_template.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CA_Jason_Taylor_template.Application.Common.Interfaces;

public interface IApplicationDbContext
{
    DbSet<TodoList> TodoLists { get; }

    DbSet<TodoItem> TodoItems { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
