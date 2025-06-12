using BugTracking.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace BugTracking.Application.Data;

public interface IBugTrackingDbContext
{
    DbSet<BugReport> BugReports { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
