using Application.Abstractions.Messaging;
using Domain.Reports;

namespace Application.Reports.Complete;

public sealed record PatchStatusCommand
    (Guid ReportId, Status Status) : ICommand;
