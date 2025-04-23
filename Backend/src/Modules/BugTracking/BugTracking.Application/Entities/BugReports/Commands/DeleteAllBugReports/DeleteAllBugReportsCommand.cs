using BuildingBlocks.Application.Cqrs;

namespace BugTracking.Application.Entities.BugReports.Commands.DeleteAllBugReports;

public record DeleteAllBugReportsCommand : ICommand<DeleteAllBugReportsResult>;

public record DeleteAllBugReportsResult(List<string> BugReportIds);