using BuildingBlocks.Application.Cqrs;

namespace BugTracking.Application.Entities.BugReports.Commands.CreateIssues;

public record CreateIssuesCommand : ICommand<CreateIssuesResult>;

// ReSharper disable once NotAccessedPositionalProperty.Global
public record CreateIssuesResult(List<string> ProcessedBugReportIds);
