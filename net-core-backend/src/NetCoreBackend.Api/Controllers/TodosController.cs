using Microsoft.AspNetCore.Mvc;
using NetCoreBackend.Application.Services.Interface;
using NetCoreBackend.Domain.Entities;

namespace NetCoreBackend.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TodosController : ControllerBase
{
    private readonly ITodoService _todoService;

    public TodosController(ITodoService todoService)
    {
        _todoService = todoService;
    }

    [HttpGet]
    public async Task<ActionResult<IReadOnlyCollection<TodoItem>>> Get(CancellationToken cancellationToken)
    {
        var items = await _todoService.GetTodosAsync(cancellationToken);
        return Ok(items);
    }

    [HttpPost]
    public async Task<ActionResult<TodoItem>> Post([FromBody] CreateTodoRequest request, CancellationToken cancellationToken)
    {
        var item = await _todoService.CreateTodoAsync(request.Title, cancellationToken);
        return CreatedAtAction(nameof(Get), new { id = item.Id }, item);
    }
}

public record CreateTodoRequest(string Title);
