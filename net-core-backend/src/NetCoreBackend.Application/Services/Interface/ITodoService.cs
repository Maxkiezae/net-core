using NetCoreBackend.Domain.Entities;

namespace NetCoreBackend.Application.Services.Interface;

public interface ITodoService
{
    Task<IReadOnlyCollection<TodoItem>> GetTodosAsync(CancellationToken cancellationToken = default);
    Task<TodoItem> CreateTodoAsync(string title, CancellationToken cancellationToken = default);
}
