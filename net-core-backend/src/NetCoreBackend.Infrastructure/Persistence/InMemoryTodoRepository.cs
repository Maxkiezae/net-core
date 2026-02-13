
using NetCoreBackend.Domain.Entities;
using NetCoreBackend.Domain.Interface;

namespace NetCoreBackend.Infrastructure.Persistence;

public class InMemoryTodoRepository : ITodoRepository
{
    private static readonly List<TodoItem> Items = new();

    public Task<IReadOnlyCollection<TodoItem>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        IReadOnlyCollection<TodoItem> snapshot = Items
            .OrderByDescending(x => x.CreatedAtUtc)
            .ToArray();

        return Task.FromResult(snapshot);
    }

    public Task<TodoItem> AddAsync(TodoItem item, CancellationToken cancellationToken = default)
    {
        Items.Add(item);
        return Task.FromResult(item);
    }
}
