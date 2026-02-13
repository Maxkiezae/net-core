namespace NetCoreBackend.Domain.Entities;

public class TodoItem
{
    public Guid Id { get; private set; }
    public string Title { get; private set; }
    public bool IsCompleted { get; private set; }
    public DateTime CreatedAtUtc { get; private set; }

    private TodoItem()
    {
        Id = Guid.Empty;
        Title = string.Empty;
    }

    public TodoItem(string title)
    {
        if (string.IsNullOrWhiteSpace(title))
        {
            throw new ArgumentException("Title is required.", nameof(title));
        }

        Id = Guid.NewGuid();
        Title = title.Trim();
        IsCompleted = false;
        CreatedAtUtc = DateTime.UtcNow;
    }

    public void Complete()
    {
        IsCompleted = true;
    }
}
