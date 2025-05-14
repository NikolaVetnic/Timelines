namespace BugTracking.Api.Controllers.BugReports;

public record CreateBugReportRequest
{
    public string Title { get; set; }
    public string Description { get; set; }
    public string ReporterName { get; set; }
}
