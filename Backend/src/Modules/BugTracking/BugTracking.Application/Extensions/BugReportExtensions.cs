using BugTracking.Domain.Models;
using BuildingBlocks.Domain.BugTracking.BugReport.Dto;

namespace BugTracking.Application.Extensions;

public static class BugReportExtensions
{
    public static BugReportBaseDto ToBugReportDto(this BugReport bugReport)
    {
        return new BugReportBaseDto(
            bugReport.Id.ToString(),
            bugReport.Title,
            bugReport.Description,
            bugReport.ReporterName);
    }
}
