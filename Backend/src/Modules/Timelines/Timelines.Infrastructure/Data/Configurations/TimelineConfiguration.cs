using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Timelines.Infrastructure.Data.Configurations;

public class TimelineConfiguration : IEntityTypeConfiguration<Timeline>
{
    public void Configure(EntityTypeBuilder<Timeline> builder)
    {
        builder.HasKey(t => t.Id);
        builder.Property(t => t.Id).HasConversion(
            timelineId => timelineId.Value,
            dbId => TimelineId.Of(dbId));

        // ToDo: Add remaining Timeline configuration commands
    }
}
