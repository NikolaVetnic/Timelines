namespace Timelines.Application.Data;

public interface ITimelinesDbContext
{
    DbSet<Timeline> Timelines { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
