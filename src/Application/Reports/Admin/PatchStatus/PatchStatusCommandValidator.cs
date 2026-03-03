using FluentValidation;

namespace Application.Reports.Complete;

internal sealed class PatchStatusCommandValidator : AbstractValidator<ResolveReportCommand>
{
    public PatchStatusCommandValidator()
    {
        RuleFor(c => c.ReportId).NotEmpty();
    }
}
