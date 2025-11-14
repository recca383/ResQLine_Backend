using SharedKernel;

namespace Domain.Todos.Events;

public sealed record ReportDeletedDomainEvent(Guid TodoItemId) : IDomainEvent;
