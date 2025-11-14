using Application.Abstractions.Authentication;
using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Domain.Todos;
using Domain.Todos.Events;
using Microsoft.EntityFrameworkCore;
using SharedKernel;

namespace Application.Todos.Complete;

internal sealed class CompleteReportCommandHandler(
    IApplicationDbContext context,
    IDateTimeProvider dateTimeProvider,
    IUserContext userContext)
    : ICommandHandler<CompleteReportCommand>
{
    public async Task<Result> Handle(CompleteReportCommand command, CancellationToken cancellationToken)
    {
        Report? todoItem = await context.Reports
            .SingleOrDefaultAsync(t => t.Id == command.TodoItemId && t.ReportedBy == userContext.UserId, cancellationToken);

        if (todoItem is null)
        {
            return Result.Failure(ReportErrors.NotFound(command.TodoItemId));
        }

        if (todoItem.IsDeleted)
        {
            return Result.Failure(ReportErrors.AlreadyCompleted(command.TodoItemId));
        }

        todoItem.IsDeleted = true;
        todoItem.DateDeleted = dateTimeProvider.UtcNow;

        todoItem.Raise(new ReportCompletedDomainEvent(todoItem.Id));

        await context.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
