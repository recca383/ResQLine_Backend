using FluentValidation;

namespace Application.Reports.Complete;

internal sealed class ResolveReportCommandValidator : AbstractValidator<ResolveReportCommand>
{
    public ResolveReportCommandValidator()
    {
        RuleFor(c => c.ReportId).NotEmpty();
    }
}
