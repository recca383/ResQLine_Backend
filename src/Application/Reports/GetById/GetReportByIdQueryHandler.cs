using Application.Abstractions.Authentication;
using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Domain.Reports;
using Microsoft.EntityFrameworkCore;
using SharedKernel;

namespace Application.Reports.GetById;

internal sealed class GetReportByIdQueryHandler(IApplicationDbContext context, IUserContext userContext)
    : IQueryHandler<GetReportByIdQuery, ReportResponse>
{
    public async Task<Result<ReportResponse>> Handle(GetReportByIdQuery query, CancellationToken cancellationToken)
    {
        ReportResponse? report = await context.Reports
            .AsNoTracking()
            .Where(reportitem => reportitem.Id == query.ReportId && reportitem.ReportedBy == userContext.UserId)
            .Select(reportitem => new ReportResponse
            {
                Id = reportitem.Id,
                Image = reportitem.Image,
                Category = reportitem.Category,
                Description = reportitem.Description,
                Location = reportitem.ReportedAt,
            })
            .SingleOrDefaultAsync(cancellationToken);

        if (report is null)
        {
            return Result.Failure<ReportResponse>(ReportErrors.NotFound(query.ReportId));
        }

        return report;
    }
}
