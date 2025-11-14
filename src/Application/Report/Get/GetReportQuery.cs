using Application.Abstractions.Messaging;

namespace Application.Todos.Get;

public sealed record GetReportQuery(Guid UserId) : IQuery<List<ReportResponse>>;
