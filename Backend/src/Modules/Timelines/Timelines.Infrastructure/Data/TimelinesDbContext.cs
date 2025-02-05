using System.Reflection;
using Timelines.Application.Data.Abstractions;

namespace Timelines.Infrastructure.Data;

public class TimelinesDbContext(DbContextOptions<TimelinesDbContext> options) :
    DbContext(options), ITimelinesDbContext
{
    public DbSet<Timeline> Timelines { get; init; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        // Specify schema for this module
        builder.HasDefaultSchema("Timelines");

        // Configure entities
        builder.Entity<Timeline>(entity =>
        {
            entity.ToTable("Timelines"); // Specify table name within the schema

            entity.HasKey(t => t.Id);
            entity.Property(t => t.Title).IsRequired();

            // Map the ReminderIds as a collection of IDs
            entity.Ignore(t => t.NodeIds); // This prevents EF from expecting a navigation property
            entity.Property(t => t.Id).ValueGeneratedNever();  // Ensures IDs are managed externally

            entity.Property(t => t.NodeIds)
                .HasConversion(new NodeIdListConverter())
                .HasColumnName("NodeIds")
                .IsRequired(false);
        });

        // Apply all configurations taken from classes that implement IEntityTypeConfiguration<>
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(builder);
    }
}

