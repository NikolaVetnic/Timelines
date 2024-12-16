using System.Reflection;
using Nodes.Application.Data;

namespace Nodes.Infrastructure.Data;

public class NodesDbContext(DbContextOptions<NodesDbContext> options) :
    DbContext(options), INodesDbContext
{
    public DbSet<Node> Nodes { get; init; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        // Specify schema for this module
        builder.HasDefaultSchema("Nodes");

        // Configure entities
        builder.Entity<Node>(entity =>
        {
            entity.ToTable("Nodes"); // Specify table name within the schema
        });

        // Apply all configurations taken from classes that implement IEntityTypeConfiguration<>
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(builder);
    }
}
