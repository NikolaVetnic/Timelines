namespace Nodes.Application.Data;

public interface INodesDbContext
{
    DbSet<Node> Nodes { get; }
    
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}