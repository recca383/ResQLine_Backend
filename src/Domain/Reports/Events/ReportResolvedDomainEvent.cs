using SharedKernel;

namespace Domain.Reports.Events;

public sealed record ReportResolvedDomainEvent(Guid TodoItemId) : IDomainEvent;
