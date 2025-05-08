namespace Nodes.Application.Data.Abstractions;

public interface INodesDbContext
{
    DbSet<Node> Nodes { get; }
    DbSet<Phase> Phases { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
