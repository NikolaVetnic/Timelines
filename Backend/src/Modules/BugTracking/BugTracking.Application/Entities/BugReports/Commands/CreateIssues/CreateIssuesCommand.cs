using BuildingBlocks.Application.Cqrs;

namespace BugTracking.Application.Entities.BugReports.Commands.CreateIssues;

public record CreateIssuesCommand : ICommand<CreateIssuesResult>;

public record CreateIssuesResult(List<string> ProcessedBugReportIds);