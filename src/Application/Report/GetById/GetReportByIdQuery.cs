using Application.Abstractions.Messaging;

namespace Application.Todos.GetById;

public sealed record GetReportByIdQuery(Guid TodoItemId) : IQuery<ReportResponse>;
