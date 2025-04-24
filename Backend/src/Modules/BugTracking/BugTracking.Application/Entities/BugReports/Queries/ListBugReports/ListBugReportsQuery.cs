using BuildingBlocks.Application.Cqrs;
using BuildingBlocks.Domain.BugTracking.BugReport.Dto;

namespace BugTracking.Application.Entities.BugReports.Queries.ListBugReports;

public record ListBugReportsQuery : IQuery<ListBugReportsResult>;

public record ListBugReportsResult(List<BugReportBaseDto> BugReports);
