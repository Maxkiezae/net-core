
using NetCoreBackend.Application.Services.Interface;
using NetCoreBackend.Domain.Entities;
using NetCoreBackend.Domain.Interface;

namespace NetCoreBackend.Application.Services.Service;

public class TodoService : ITodoService
{
    private readonly ITodoRepository _todoRepository;

    public TodoService(ITodoRepository todoRepository)
    {
        _todoRepository = todoRepository;
    }

    public Task<IReadOnlyCollection<TodoItem>> GetTodosAsync(CancellationToken cancellationToken = default)
    {
        return _todoRepository.GetAllAsync(cancellationToken);
    }

    public async Task<TodoItem> CreateTodoAsync(string title, CancellationToken cancellationToken = default)
    {
        var item = new TodoItem(title);
        return await _todoRepository.AddAsync(item, cancellationToken);
    }
}
