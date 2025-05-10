using System.Reflection;
using BuildingBlocks.Domain.Nodes.Phase.ValueObjects;
using BuildingBlocks.Domain.Timelines.Timeline.ValueObjects;
using Nodes.Application.Data.Abstractions;

namespace Nodes.Infrastructure.Data;

public class NodesDbContext(DbContextOptions<NodesDbContext> options) : DbContext(options), INodesDbContext
{
    public DbSet<Node> Nodes { get; init; }
    public DbSet<Phase> Phases { get; init; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        // Specify schema for this module
        builder.HasDefaultSchema("Nodes");

        // Configure entities
        builder.Entity<Node>(entity =>
        {
            entity.ToTable("Nodes"); // Specify table name within the schema

            entity.HasKey(n => n.Id);
            entity.Property(n => n.Title).IsRequired();
            entity.Property(n => n.Description).IsRequired();
            entity.Property(n => n.Timestamp).IsRequired();
            entity.Property(n => n.Importance).IsRequired();
            entity.Property(n => n.PhaseId).IsRequired();

            // Map the ReminderIds as a collection of IDs
            entity.Ignore(n => n.ReminderIds); // This prevents EF from expecting a navigation property
            entity.Property(n => n.Id).ValueGeneratedNever(); // Ensures IDs are managed externally

            entity.Property(e => e.ReminderIds)
                .HasConversion(new ReminderIdListConverter())
                .HasColumnName("ReminderIds")
                .IsRequired(false);

            entity.Property(r => r.TimelineId).IsRequired();
            entity.HasIndex(r => r.TimelineId); // Add an index for efficient querying
            entity.Property(r => r.TimelineId)
                .HasConversion(new TimelineIdValueConverter()) // Apply the value converter
                .IsRequired();

            // Map the FileAssetIds as a collection of IDs
            entity.Ignore(n => n.FileAssetIds); // This prevents EF from expecting a navigation property
            entity.Property(n => n.Id).ValueGeneratedNever(); // Ensures IDs are managed externally

            entity.Property(e => e.FileAssetIds)
                .HasConversion(new FileAssetIdListConverter())
                .HasColumnName("FileAssetIds")
                .IsRequired(false);

            // Map the NoteIds as a collection of IDs
            entity.Ignore(n => n.NoteIds); // This prevents EF from expecting a navigation property
            entity.Property(n => n.Id).ValueGeneratedNever(); // Ensures IDs are managed externally

            entity.Property(e => e.NoteIds)
                .HasConversion(new NoteIdListConverter())
                .HasColumnName("NoteIds")
                .IsRequired(false);

            entity.Property(r => r.PhaseId).IsRequired();
            entity.HasIndex(r => r.PhaseId); // Add an index for efficient querying
            entity.Property(r => r.PhaseId)
                .HasConversion(new PhaseIdValueConverter()) // Apply the value converter
                .IsRequired();
        });

        builder.Entity<Phase>(entity =>
        {
            entity.ToTable("Phases"); // Specify table name within the schema

            entity.HasKey(p => p.Id);
            entity.Property(p => p.Id)
                .HasConversion(new PhaseIdValueConverter());

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

            // Map the NoteIds as a collection of IDs
            entity.Ignore(n => n.NodeIds); // This prevents EF from expecting a navigation property
            entity.Property(n => n.Id).ValueGeneratedNever(); // Ensures IDs are managed externally

            entity.Property(e => e.NodeIds)
                .HasConversion(new NodeIdListConverter())
                .HasColumnName("NodeIds")
                .IsRequired(false);

        });

        // Apply all configurations taken from classes that implement IEntityTypeConfiguration<>
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(builder);
    }
}
