using FluentValidation;

namespace Application.Reports.Create;

public class CreateReportCommandValidator : AbstractValidator<CreateReportCommand>
{
    public CreateReportCommandValidator()
    {
        RuleFor(c => c.Category).IsInEnum();
        RuleFor(c => c.Description).NotEmpty().MaximumLength(255);
    }
}
