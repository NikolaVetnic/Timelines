using BuildingBlocks.Domain.Nodes.Phase.ValueObjects;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Nodes.Infrastructure.Data.Configurations;

public class PhaseConfiguration : IEntityTypeConfiguration<Phase>
{
    public void Configure(EntityTypeBuilder<Phase> builder)
    {
        builder.HasKey(p => p.Id);
        builder.Property(p => p.Id).HasConversion(
            phaseId => phaseId.Value,
            dbId => PhaseId.Of(dbId));

        builder.Property(n => n.Title)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(n => n.Description)
            .IsRequired()
            .HasMaxLength(500);
    }
}
