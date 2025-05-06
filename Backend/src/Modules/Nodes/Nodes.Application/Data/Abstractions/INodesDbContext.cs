namespace Nodes.Application.Data.Abstractions;

public interface INodesDbContext
{
    DbSet<Node> Nodes { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
