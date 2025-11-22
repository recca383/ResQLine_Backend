using SharedKernel;

namespace Domain.Reports;

public static class ReportErrors
{
    public static Error AlreadyResolved(Guid reportId) => Error.Problem(
        "Report.AlreadyResolved",
        $"The report with Id = '{reportId}' is already resolved.");

    public static Error NotFound(Guid reportId) => Error.NotFound(
        "Report.NotFound",
        $"The report with the Id = '{reportId}' was not found");

    public static Error AlreadyDeleted(Guid reportId) => Error.Problem(
        "Report.AlreadyDeleted",
        $"The report with the Id = '{reportId}' is already deleted."
        );
}
