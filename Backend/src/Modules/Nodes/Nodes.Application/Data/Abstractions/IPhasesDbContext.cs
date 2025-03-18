namespace Nodes.Application.Data.Abstractions;

public interface IPhasesDbContext
{
    DbSet<Phase> Phases { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
