using BugTracking.Application.Data;
using BugTracking.Application.Extensions;
using BuildingBlocks.Application.Cqrs;
using Microsoft.EntityFrameworkCore;

namespace BugTracking.Application.Entities.BugReports.Queries.ListBugReports;

public class ListBugReportsHandler(IBugTrackingDbContext dbContext)
    : IQueryHandler<ListBugReportsQuery, ListBugReportsResult>
{
    public async Task<ListBugReportsResult> Handle(ListBugReportsQuery request, CancellationToken cancellationToken)
    {
        var bugReports = await dbContext.BugReports
            .AsNoTracking()
            .OrderBy(br => br.CreatedAt)
            .ToListAsync(cancellationToken: cancellationToken);

        return new ListBugReportsResult(bugReports.Select(br => br.ToBugReportDto()).ToList());
    }
}
