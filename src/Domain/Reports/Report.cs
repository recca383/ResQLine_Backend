using SharedKernel;

namespace Domain.Reports;

public sealed class Report : Entity
{
    public Guid Id { get; set; }
    public Guid  ReportedBy { get; set; }
    public byte[] Image { get; set; }
    public string Title { get; set; }
    public string? Description { get; set; }
    public Location ReportedAt { get; set; }
    public Status Status { get; set; }
    public Guid Category { get; set; }
    public bool IsDeleted { get; set; }
    public DateTime DateCreated { get; set; }
    public DateTime? DateLastUpdated { get; set; }
    public DateTime? DateDeleted { get; set; }
    public Priority Priority { get; set; }
    
    
}
