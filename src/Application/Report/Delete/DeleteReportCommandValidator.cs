using FluentValidation;

namespace Application.Todos.Delete;

internal sealed class DeleteReportCommandValidator : AbstractValidator<DeleteReportCommand>
{
    public DeleteReportCommandValidator()
    {
        RuleFor(c => c.TodoItemId).NotEmpty();
    }
}
