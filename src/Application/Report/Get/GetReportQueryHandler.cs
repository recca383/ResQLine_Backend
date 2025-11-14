using Application.Abstractions.Authentication;
using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Domain.Users;
using Microsoft.EntityFrameworkCore;
using SharedKernel;

namespace Application.Todos.Get;

internal sealed class GetReportQueryHandler(IApplicationDbContext context, IUserContext userContext)
    : IQueryHandler<GetReportQuery, List<ReportResponse>>
{
    public async Task<Result<List<ReportResponse>>> Handle(GetReportQuery query, CancellationToken cancellationToken)
    {
        if (query.UserId != userContext.UserId)
        {
            return Result.Failure<List<ReportResponse>>(UserErrors.Unauthorized());
        }

        List<ReportResponse> todos = await context.Reports
            .Where(todoItem => todoItem.ReportedBy == query.UserId)
            .Select(todoItem => new ReportResponse
            {
                Id = todoItem.Id,
                UserId = todoItem.ReportedBy,
                Description = todoItem.Description,
                IsCompleted = todoItem.IsDeleted,
                CreatedAt = todoItem.DateCreated,
                CompletedAt = todoItem.DateDeleted
            })
            .ToListAsync(cancellationToken);

        return todos;
    }
}
