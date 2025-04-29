namespace Nodes.Application.Data.Abstractions.Phases;

internal interface IPhasesDbContext
{
    DbSet<Phase> Phases { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
