namespace Nodes.Application.Data.Abstractions.Phases;

public interface IPhasesDbContext
{
    DbSet<Phase> Phases { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
