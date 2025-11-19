using SharedKernel;

namespace Domain.Reports.Events;

public sealed record ReportCreatedDomainEvent(Guid TodoItemId) : IDomainEvent;
