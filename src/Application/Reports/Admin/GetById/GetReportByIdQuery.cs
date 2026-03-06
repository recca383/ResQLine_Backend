using Application.Abstractions.Messaging;

namespace Application.Reports.Admin.GetById;

public sealed record GetReportByIdQuery(Guid ReportId) : IQuery<ReportResponse>;
