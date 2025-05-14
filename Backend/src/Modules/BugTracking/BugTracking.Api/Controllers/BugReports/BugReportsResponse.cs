using System.Collections.Generic;
using BuildingBlocks.Domain.BugTracking.BugReport.Dto;
using BuildingBlocks.Domain.BugTracking.BugReport.ValueObject;

namespace BugTracking.Api.Controllers.BugReports;

public record CreateBugReportResponse(BugReportId Id);

public record CreateIssuesResponse(List<string> ProcessedBugReportIds);

public record ListBugReportsResponse(List<BugReportBaseDto> BugReports);

public record DeleteAllBugReportsResponse(List<string> BugReportIds);
