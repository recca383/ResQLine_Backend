using Application.Abstractions.Messaging;

namespace Application.Reports.Delete;

public sealed record DeleteReportCommand(Guid ReportId) : ICommand;
