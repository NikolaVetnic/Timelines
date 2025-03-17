namespace Nodes.Application.Data.Abstractions;

public interface IPhaseDbContext
{
    DbSet<Phase> Phase { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
