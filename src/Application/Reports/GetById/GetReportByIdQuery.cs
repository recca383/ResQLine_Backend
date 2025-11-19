using Application.Abstractions.Messaging;

namespace Application.Reports.GetById;

public sealed record GetReportByIdQuery(Guid TodoItemId) : IQuery<ReportResponse>;
