using FluentValidation;

namespace Application.Todos.Complete;

internal sealed class CompleteReportCommandValidator : AbstractValidator<CompleteReportCommand>
{
    public CompleteReportCommandValidator()
    {
        RuleFor(c => c.TodoItemId).NotEmpty();
    }
}
