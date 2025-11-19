using Application.Abstractions.Authentication;
using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Domain.Reports;
using Domain.Reports.Events;
using Microsoft.EntityFrameworkCore;
using SharedKernel;

namespace Application.Reports.Complete;

internal sealed class ResolveReportCommandHandler(
    IApplicationDbContext context,
    IDateTimeProvider dateTimeProvider,
    IUserContext userContext)
    : ICommandHandler<ResolveReportCommand>
{
    public async Task<Result> Handle(ResolveReportCommand command, CancellationToken cancellationToken)
    {
        Report? report = await context.Reports
            .SingleOrDefaultAsync(t => t.Id == command.ReportId && t.ReportedBy == userContext.UserId, cancellationToken);

        if (report is null)
        {
            return Result.Failure(ReportErrors.NotFound(command.ReportId));
        }

        if (report.Status == Status.Resolved)
        {
            return Result.Failure(ReportErrors.AlreadyResolved(command.ReportId));
        }

        
        report.DateResolved = dateTimeProvider.UtcNow;

        report.Raise(new ReportResolvedDomainEvent(report.Id));

        await context.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
