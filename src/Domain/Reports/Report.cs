using System.Globalization;
using Domain.Users;
using SharedKernel;

namespace Domain.Reports;

public sealed class Report : Entity
{
    public Guid Id { get; set; }
    public Guid  ReportedById { get; set; }
    public User ReportedBy { get; set; }
    public List<byte[]> Image { get; set; }
    public string? Description { get; set; }
    public Location ReportedAt { get; set; }
    public Status Status { get; set; }
    public Category Category { get; set; }
    public bool IsDeleted { get; set; }
    public Dictionary<string, float> AIProbabilities { get; set; } = new();
    public DateTime DateCreated { get; set; }
    public DateTime? DateLastUpdated { get; set; }
    public DateTime? DateResolved { get; set; }
    public Priority Priority { get; set; }

    public bool IsActive => !IsDeleted;

    public string GetCategoryString() => Category switch
    {
        Category.Fire_Incident => "Fire",
        Category.Flooding => "Flooding",
        Category.Medical_Emergency => "Medical Emergency",
        Category.Structural_Damage => "Structural Damage",
        Category.Traffic_Accident => "Traffic Accident",
        Category.Other_General_Incident => "",
        _ => "Unknown"
    };


}


