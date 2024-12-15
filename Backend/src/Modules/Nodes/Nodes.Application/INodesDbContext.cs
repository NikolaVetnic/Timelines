using Microsoft.EntityFrameworkCore;
using Nodes.Domain.Models;

namespace Nodes.Application;

public interface INodesDbContext
{
    DbSet<Node> Nodes { get; }
    
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}