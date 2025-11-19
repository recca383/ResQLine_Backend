using Application.Abstractions.Messaging;

namespace Application.Reports.Get;

public sealed record GetReportQuery(Guid UserId) : IQuery<List<ReportResponse>>;
