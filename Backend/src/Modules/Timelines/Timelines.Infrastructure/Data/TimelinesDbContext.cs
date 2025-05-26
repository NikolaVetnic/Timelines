using BuildingBlocks.Domain.Timelines.Phase.ValueObjects;
using BuildingBlocks.Domain.Timelines.Timeline.ValueObjects;
using System.Reflection;
using Timelines.Application.Data.Abstractions;

namespace Timelines.Infrastructure.Data;

public class TimelinesDbContext(DbContextOptions<TimelinesDbContext> options) :
    DbContext(options), ITimelinesDbContext
{
    public DbSet<Timeline> Timelines { get; init; }

    public DbSet<Phase> Phases { get; init; }

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

            // Map the NodeIds as a collection of IDs
            entity.Ignore(t => t.NodeIds); // This prevents EF from expecting a navigation property
            entity.Property(t => t.Id).ValueGeneratedNever();  // Ensures IDs are managed externally

            entity.Property(t => t.NodeIds)
                .HasConversion(new NodeIdListConverter())
                .HasColumnName("NodeIds")
                .IsRequired(false);

            // Map the PhaseIds as a collection of IDs
            entity.Ignore(t => t.PhaseIds); // This prevents EF from expecting a navigation property
            entity.Property(t => t.Id).ValueGeneratedNever();  // Ensures IDs are managed externally

            entity.Property(t => t.PhaseIds)
                .HasConversion(new PhaseIdListConverter())
                .HasColumnName("PhaseIds")
                .IsRequired(false);
        });

        builder.Entity<Phase>(entity =>
        {
            entity.ToTable("Phases"); // Specify table name within the schema

            entity.HasKey(p => p.Id);
            entity.Property(p => p.Id)
                .HasConversion(new PhaseIdValueConverter());

            entity.Property(r => r.TimelineId).IsRequired();
            entity.HasIndex(r => r.TimelineId); // Add an index for efficient querying
            entity.Property(r => r.TimelineId)
                .HasConversion(new TimelineIdValueConverter()) // Apply the value converter
                .IsRequired();

            entity.Property(p => p.Title).IsRequired();
            entity.Property(p => p.Description).IsRequired();
            entity.Property(p => p.StartDate).IsRequired();
            entity.Property(p => p.EndDate).IsRequired(false);
            entity.Property(p => p.Duration).IsRequired(false);
            entity.Property(p => p.Status).IsRequired();
            entity.Property(p => p.Progress).IsRequired();
            entity.Property(p => p.IsCompleted).IsRequired();

            entity.Property(p => p.Parent)
                .HasConversion(new PhaseIdValueConverter())
                .IsRequired();

            entity.Property(p => p.DependsOn)
                .HasConversion(new DependsOnPhaseIdListConverter())
                .HasColumnName("DependsOn")
                .IsRequired(false);
        });

        // Apply all configurations taken from classes that implement IEntityTypeConfiguration<>
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(builder);
    }
}

