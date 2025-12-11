using Application.Abstractions.Messaging;
using Domain.Reports;

namespace Application.Reports.Create;

public sealed class CreateReportCommand : ICommand<Guid>
{
    public byte[] Image { get; set; }
    public Category Category { get; set; } = Category.None;
    public string? Description { get; set; }
    public Location ReportedAt { get; set; }

}
