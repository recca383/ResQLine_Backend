using Application.Abstractions.Authentication;
using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Domain.Todos;
using Domain.Todos.Events;
using Microsoft.EntityFrameworkCore;
using SharedKernel;

namespace Application.Todos.Delete;

internal sealed class DeleteReportCommandHandler(IApplicationDbContext context, IUserContext userContext)
    : ICommandHandler<DeleteReportCommand>
{
    public async Task<Result> Handle(DeleteReportCommand command, CancellationToken cancellationToken)
    {
        Report? todoItem = await context.Reports
            .SingleOrDefaultAsync(t => t.Id == command.TodoItemId && t.ReportedBy == userContext.UserId, cancellationToken);

        if (todoItem is null)
        {
            return Result.Failure(ReportErrors.NotFound(command.TodoItemId));
        }

        context.Reports.Remove(todoItem);

        todoItem.Raise(new ReportDeletedDomainEvent(todoItem.Id));

        await context.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
