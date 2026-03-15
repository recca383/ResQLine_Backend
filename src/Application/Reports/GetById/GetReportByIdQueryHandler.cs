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
            .Include(r => r.ReportedBy)
            .AsNoTracking()
            .Where(reportitem => reportitem.Id == query.ReportId && reportitem.ReportedById == userContext.UserId)
            .Select(report => new ReportResponse
            {
                Id = report.Id,
                Images = report.Image,
                Category = report.Category,
                Description = report.Description,
                Location = report.ReportedAt,
                AIProbabilities = report.AIProbabilities,
                ReportByName = report.ReportedBy.FirstName + " "
                             + report.ReportedBy.LastName,
                ReportByPhoneNumber = report.ReportedBy.MobileNumber
            })
            .SingleOrDefaultAsync(cancellationToken);

        if (report is null)
        {
            return Result.Failure<ReportResponse>(ReportErrors.NotFound(query.ReportId));
        }

        return report;
    }
}
