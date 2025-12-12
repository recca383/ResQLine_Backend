using SharedKernel;

namespace Domain.Reports.Events;

public sealed record ReportCreatedDomainEvent(Report report) : IDomainEvent;
