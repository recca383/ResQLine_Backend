using SharedKernel;

namespace Domain.Reports.Events;

public sealed record ReportDeletedDomainEvent(Guid TodoItemId) : IDomainEvent;
