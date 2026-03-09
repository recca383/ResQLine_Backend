using Application.Abstractions.Authentication;
using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Domain.Reports;
using Domain.Users;
using Microsoft.EntityFrameworkCore;
using SharedKernel;

namespace Application.Reports.Admin.Get;

internal sealed class GetReportQueryHandler(IApplicationDbContext context)
    : IQueryHandler<GetReportQuery, List<ReportResponse>>
{
    public async Task<Result<List<ReportResponse>>> Handle(GetReportQuery query, CancellationToken cancellationToken)
    {
        List<ReportResponse> reports = await context.Reports
            .AsNoTracking()
            .Select(report => new ReportResponse
            {
                Id = report.Id,
                Description = report.Description!,
                Images = report.Image,
                Category = report.Category,
                Location = report.ReportedAt,
                CreatedAt = report.DateCreated,
                Status = report.Status,
                AIProbabilities = report.AIProbabilities,
                
            })
            .OrderByDescending(r => r.CreatedAt)
            .Skip(Math.Max(0,query.pageoffset - 1) * query.pageSize)
            .Take(query.pageSize)
            .ToListAsync(cancellationToken);
            
        return reports;
    }
}
