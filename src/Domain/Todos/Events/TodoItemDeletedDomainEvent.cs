using SharedKernel;

namespace Domain.Todos.Events;

public sealed record TodoItemDeletedDomainEvent(Guid TodoItemId) : IDomainEvent;
