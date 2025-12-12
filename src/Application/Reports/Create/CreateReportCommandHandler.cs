using Application.Abstractions.Authentication;
using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Domain.Reports;
using Domain.Reports.Events;
using Domain.Users;
using Microsoft.EntityFrameworkCore;
using SharedKernel;

namespace Application.Reports.Create;

internal sealed class CreateReportCommandHandler(
    IApplicationDbContext context,
    IDateTimeProvider dateTimeProvider,
    IUserContext userContext)
    : ICommandHandler<CreateReportCommand, Guid>
{
    public async Task<Result<Guid>> Handle(CreateReportCommand command, CancellationToken cancellationToken)
    {
        User? user = await context.Users.AsNoTracking()
            .SingleOrDefaultAsync(u => u.Id == userContext.UserId, cancellationToken);

        if (user is null)
        {
            return Result.Failure<Guid>(UserErrors.NotFound(userContext.UserId));
        }

        var reportId = new Report
        {
            Id = Guid.NewGuid(),
            ReportedBy = user.Id,
            Image = command.Image,
            Description = command.Description,
            ReportedAt = command.ReportedAt,
            Status = Status.Under_Review,
            Category = command.Category,
            IsDeleted = false,
            DateCreated = dateTimeProvider.UtcNow,
            Priority = Priority.Low
        };

        // Send an email to the department and the user who reported it
        reportId.Raise(new ReportCreatedDomainEvent(reportId));

        context.Reports.Add(reportId);

        await context.SaveChangesAsync(cancellationToken);

        return reportId.Id;
    }
}
