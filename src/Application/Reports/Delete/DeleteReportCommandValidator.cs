using FluentValidation;

namespace Application.Reports.Delete;

internal sealed class DeleteReportCommandValidator : AbstractValidator<DeleteReportCommand>
{
    public DeleteReportCommandValidator()
    {
        RuleFor(c => c.ReportId).NotEmpty();
    }
}
