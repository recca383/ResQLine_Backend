using Domain.Users;
using SharedKernel;

namespace Domain.Todos;

public sealed class TodoItem : Entity
{
    public Guid Id { get; set; }
    public Guid RequestedBy { get; set; }
    public Guid? AssignedTo { get; set; }
    public Team TeamAssignedTo { get; set; }
    public Status Status { get; set; }
    public string Description { get; set; }
    public DateTime? DueDate { get; set; }
    public List<string> Labels { get; set; } = [];
    public bool IsCompleted { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? CompletedAt { get; set; }
    public Priority Priority { get; set; }
}
