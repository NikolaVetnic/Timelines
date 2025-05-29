using BugTracking.Application.Data;
using BuildingBlocks.Application.Cqrs;
using Microsoft.EntityFrameworkCore;

namespace BugTracking.Application.Entities.BugReports.Commands.DeleteAllBugReports;

public class DeleteAllBugReportsHandler(IBugTrackingDbContext dbContext) : ICommandHandler<DeleteAllBugReportsCommand, DeleteAllBugReportsResult>
{
    public async Task<DeleteAllBugReportsResult> Handle(DeleteAllBugReportsCommand request, CancellationToken cancellationToken)
    {
        var bugReports = await dbContext.BugReports
            .AsNoTracking()
            .OrderBy(br => br.CreatedAt)
            .ToListAsync(cancellationToken);

        var bugReportIds = bugReports.Select(br => br.Id.ToString()).ToList();

        dbContext.BugReports.RemoveRange(bugReports);
        await dbContext.SaveChangesAsync(cancellationToken);

        return new DeleteAllBugReportsResult(bugReportIds);
    }
}
