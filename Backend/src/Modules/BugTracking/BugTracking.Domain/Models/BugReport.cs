using BuildingBlocks.Domain.Abstractions;
using BuildingBlocks.Domain.BugTracking.BugReport.ValueObject;

namespace BugTracking.Domain.Models;

public class BugReport : Entity<BugReportId>
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string ReporterName { get; set; } = string.Empty; // ToDo: Substitute for UserId
    public bool IsProcessed { get; set; } = false;

    public static BugReport Create(BugReportId id, string title, string description, string reporterName)
    {
        var bugReport = new BugReport
        {
            Id = id,
            Title = title,
            Description = description,
            ReporterName = reporterName,
            IsProcessed = false
        };
        
        return bugReport;
    }
}
