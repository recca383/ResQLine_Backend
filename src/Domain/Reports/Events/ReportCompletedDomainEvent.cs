using SharedKernel;

namespace Domain.Todos.Events;

public sealed record ReportCompletedDomainEvent(Guid TodoItemId) : IDomainEvent;
