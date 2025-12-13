using Domain.Reports;

namespace Application.Reports.GetById;

public sealed class ReportResponse
{
    public Guid Id { get; set; }
    public string? Description { get; set; }
    public List<byte[]> Images { get; set; }
    public Category Category { get; set; }
    public Location Location { get; set; }
    public DateTime CreatedAt { get; set; }
    public Status Status { get; set; }
}
