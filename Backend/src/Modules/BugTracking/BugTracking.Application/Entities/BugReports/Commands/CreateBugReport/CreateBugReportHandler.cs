using BugTracking.Application.Data;
using BugTracking.Domain.Models;
using BuildingBlocks.Application.Cqrs;
using BuildingBlocks.Domain.BugTracking.BugReport.ValueObject;

namespace BugTracking.Application.Entities.BugReports.Commands.CreateBugReport;

internal class CreateBugReportHandler(IBugTrackingDbContext dbContext)
    : ICommandHandler<CreateBugReportCommand, CreateBugReportResult>
{
    public async Task<CreateBugReportResult> Handle(CreateBugReportCommand command, CancellationToken cancellationToken)
    {
        var bugReport = command.ToBugReport();
        
        dbContext.BugReports.Add(bugReport);
        await dbContext.SaveChangesAsync(cancellationToken);
        
        return new CreateBugReportResult(bugReport.Id);
    }
}

internal static class CreateBugReportCommandExtensions
{
    public static BugReport ToBugReport(this CreateBugReportCommand command)
    {
        return BugReport.Create(
            BugReportId.Of(Guid.NewGuid()),
            command.Title,
            command.Description,
            command.ReporterName
        );
    }
}
