using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Nodes.Infrastructure.Data.Configurations;

public class NodeConfiguration : IEntityTypeConfiguration<Node>
{
    public void Configure(EntityTypeBuilder<Node> builder)
    {
        builder.HasKey(n => n.Id);
        builder.Property(n => n.Id).HasConversion(
            nodeId => nodeId.Value,
            dbId => NodeId.Of(dbId));
        
        // ToDo: Add remaining Node configuration commands
    }
}