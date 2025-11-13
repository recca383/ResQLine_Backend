using SharedKernel;

namespace Domain.Todos.Events;

public sealed record TodoItemCompletedDomainEvent(Guid TodoItemId) : IDomainEvent;
