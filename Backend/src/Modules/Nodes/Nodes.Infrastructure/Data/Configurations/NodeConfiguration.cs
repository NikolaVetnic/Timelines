using BuildingBlocks.Domain.Nodes.Node.ValueObjects;
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

        builder.Property(n => n.Title)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(n => n.Description)
            .IsRequired()
            .HasMaxLength(500);

        builder.Property(n => n.Importance)
            .IsRequired();
    }
}
