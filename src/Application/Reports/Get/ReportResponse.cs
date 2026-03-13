using Domain.Reports;

namespace Application.Reports.Get;

public sealed class ReportResponse
{
    public Guid Id { get; set; }
    public string? Description { get; set; }
    public List<byte[]> Images { get; set; }
    public Category Category { get; set; }
    public Location Location { get; set; }
    public DateTime CreatedAt { get; set; }
    public Status Status { get; set; }
    public Dictionary<string, float> AIProbabilities{ get; set; }
    public string ReportByName { get; set; }
    public string ReportByPhoneNumber { get; set; }
}
