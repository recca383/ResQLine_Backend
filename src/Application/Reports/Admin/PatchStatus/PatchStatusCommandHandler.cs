using Application.Abstractions.Authentication;
using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Domain.Reports;
using Domain.Reports.Events;
using Microsoft.EntityFrameworkCore;
using SharedKernel;

namespace Application.Reports.Complete;

internal sealed class PatchStatusCommandHandler(
    IApplicationDbContext context)
    : ICommandHandler<PatchStatusCommand>
{
    public async Task<Result> Handle(PatchStatusCommand command, CancellationToken cancellationToken)
    {
        Report? report = await context.Reports
            .SingleOrDefaultAsync(t => t.Id == command.ReportId, cancellationToken);

        if (report is null)
        {
            return Result.Failure(ReportErrors.NotFound(command.ReportId));
        }

        if (report.Status == Status.Resolved)
        {
            return Result.Failure(ReportErrors.AlreadyResolved(command.ReportId));
        }


        report.Status = command.Status;

        //report.Raise(new ReportResolvedDomainEvent(report.Id));

        await context.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
