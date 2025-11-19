using Application.Abstractions.Messaging;

namespace Application.Reports.Complete;

public sealed record ResolveReportCommand
    (Guid ReportId) : ICommand;
