using SharedKernel;

namespace Domain.Todos.Events;

public sealed record TodoItemCreatedDomainEvent(Guid TodoItemId) : IDomainEvent;
