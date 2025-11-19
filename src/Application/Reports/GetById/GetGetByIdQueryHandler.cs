using Application.Abstractions.Authentication;
using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Domain.Reports;
using Microsoft.EntityFrameworkCore;
using SharedKernel;

namespace Application.Reports.GetById;

internal sealed class GetGetByIdQueryHandler(IApplicationDbContext context, IUserContext userContext)
    : IQueryHandler<GetReportByIdQuery, ReportResponse>
{
    public async Task<Result<ReportResponse>> Handle(GetReportByIdQuery query, CancellationToken cancellationToken)
    {
        ReportResponse? todo = await context.Reports
            .Where(todoItem => todoItem.Id == query.TodoItemId && todoItem.ReportedBy == userContext.UserId)
            .Select(todoItem => new ReportResponse
            {
                Id = todoItem.Id,
                UserId = todoItem.ReportedBy,
                Description = todoItem.Description,
                IsCompleted = todoItem.IsDeleted,
                CreatedAt = todoItem.DateCreated,
                CompletedAt = todoItem.DateResolved
            })
            .SingleOrDefaultAsync(cancellationToken);

        if (todo is null)
        {
            return Result.Failure<ReportResponse>(ReportErrors.NotFound(query.TodoItemId));
        }

        return todo;
    }
}
