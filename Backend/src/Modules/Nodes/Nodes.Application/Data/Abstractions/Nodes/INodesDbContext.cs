namespace Nodes.Application.Data.Abstractions.Nodes;

public interface INodesDbContext
{
    DbSet<Node> Nodes { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
