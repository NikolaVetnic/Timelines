using System.Reflection;
using BuildingBlocks.Domain.Timelines.Timeline.ValueObjects;
using Nodes.Application.Data.Abstractions;

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

            entity.HasKey(n => n.Id);
            entity.Property(n => n.Title).IsRequired();
            entity.Property(n => n.Description).IsRequired();
            entity.Property(n => n.Timestamp).IsRequired();
            entity.Property(n => n.Importance).IsRequired();
            entity.Property(n => n.Phase).IsRequired();

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
        });

        // Apply all configurations taken from classes that implement IEntityTypeConfiguration<>
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(builder);
    }
}
