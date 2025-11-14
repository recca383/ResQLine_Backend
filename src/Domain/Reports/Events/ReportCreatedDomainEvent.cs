using SharedKernel;

namespace Domain.Todos.Events;

public sealed record ReportCreatedDomainEvent(Guid TodoItemId) : IDomainEvent;
