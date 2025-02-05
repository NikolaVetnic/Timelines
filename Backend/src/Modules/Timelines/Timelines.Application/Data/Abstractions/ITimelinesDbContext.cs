namespace Timelines.Application.Data.Abstractions;

public interface ITimelinesDbContext
{
    DbSet<Timeline> Timelines { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
