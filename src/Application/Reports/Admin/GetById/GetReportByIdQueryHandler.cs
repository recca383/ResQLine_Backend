using Application.Abstractions.Authentication;
using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Domain.Reports;
using Microsoft.EntityFrameworkCore;
using SharedKernel;

namespace Application.Reports.Admin.GetById;

internal sealed class GetReportByIdQueryHandler(IApplicationDbContext context)
    : IQueryHandler<GetReportByIdQuery, ReportResponse>
{
    public async Task<Result<ReportResponse>> Handle(GetReportByIdQuery query, CancellationToken cancellationToken)
    {
        Report? report = await context.Reports
            .FirstOrDefaultAsync(r => r.Id == query.ReportId, cancellationToken);

        if (report is null)
        {
            return Result.Failure<ReportResponse>(ReportErrors.NotFound(query.ReportId));
        }

        var result = new ReportResponse
        {
            Id = report.Id,
            Images = report.Image,
            Category = report.Category,
            Description = report.Description,
            Location = report.ReportedAt,
            Status = report.Status,
            CreatedAt = report.DateCreated,
            AIProbabilities = report.AIProbabilities,
        };
        return result;
    }
}
