using Application.Abstractions.Authentication;
using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Domain.Reports;
using Domain.Reports.Events;
using Microsoft.EntityFrameworkCore;
using SharedKernel;

namespace Application.Reports.Delete;

internal sealed class DeleteReportCommandHandler(IApplicationDbContext context, IUserContext userContext)
    : ICommandHandler<DeleteReportCommand>
{
    public async Task<Result> Handle(DeleteReportCommand command, CancellationToken cancellationToken)
    {
        Report? report = await context.Reports
            .SingleOrDefaultAsync(t => t.Id == command.ReportId && t.ReportedBy == userContext.UserId, cancellationToken);

        if (report is null)
        {
            return Result.Failure(ReportErrors.NotFound(command.ReportId));
        }

        if (report.IsDeleted)
        {
            return Result.Failure(ReportErrors.AlreadyDeleted(command.ReportId));
        }    

        report.IsDeleted = true;

        report.Raise(new ReportDeletedDomainEvent(report.Id));

        await context.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
