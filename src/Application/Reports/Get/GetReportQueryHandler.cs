using Application.Abstractions.Authentication;
using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Domain.Reports;
using Domain.Users;
using Microsoft.EntityFrameworkCore;
using SharedKernel;

namespace Application.Reports.Get;

internal sealed class GetReportQueryHandler(IApplicationDbContext context, IUserContext user)
    : IQueryHandler<GetReportQuery, List<ReportResponse>>
{
    public async Task<Result<List<ReportResponse>>> Handle(GetReportQuery query, CancellationToken cancellationToken)
    {
        List<ReportResponse> reports = await context.Reports
            .Where(u => u.ReportedBy == user.UserId && !u.IsDeleted)
            .Select(report => new ReportResponse
            {
                Id = report.Id,
                Description = report.Description!,
                Image = report.Image,
                Category = report.Category,
                Location = report.ReportedAt,
                CreatedAt = report.DateCreated,
                Status = report.Status,
                
            })
            .OrderByDescending(r => r.CreatedAt)
            .Skip((query.pageoffset - 1) * query.pageSize)
            .Take(query.pageSize)
            .ToListAsync(cancellationToken);
            
        return reports;
    }
}
