using System.Reflection;
using BuildingBlocks.Domain.Nodes.Node.ValueObjects;
using Reminders.Application.Data.Abstractions;

namespace Reminders.Infrastructure.Data;

public class RemindersDbContext(DbContextOptions<RemindersDbContext> options) :
    DbContext(options), IRemindersDbContext
{
    public DbSet<Reminder> Reminders { get; init; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.HasDefaultSchema("Reminders");

        builder.Entity<Reminder>(entity =>
        {
            entity.ToTable("Reminders"); // Specify table name within the schema

            entity.HasKey(r => r.Id);
            entity.Property(r => r.Title).IsRequired();
            entity.Property(r => r.Description).IsRequired();
            entity.Property(r => r.NotifyAt).IsRequired();
            entity.Property(r => r.Priority).IsRequired();

            // Map the RelatedReminders as a collection of IDs
            entity.Ignore(r => r.RelatedReminders); // This prevents EF from expecting a navigation property
            entity.Property(r => r.Id).ValueGeneratedNever(); // Ensures IDs are managed externally

            entity.Property(e => e.RelatedReminders)
                .HasConversion(new RelatedReminderIdListConverter())
                .HasColumnName("RelatedReminders")
                .IsRequired(false);

            entity.Property(r => r.NodeId).IsRequired();
            entity.HasIndex(r => r.NodeId); // Add an index for efficient querying

            // Configure NodeId with the value converter
            entity.Property(r => r.NodeId)
                .HasConversion(new NodeIdValueConverter()) // Apply the value converter
                .IsRequired();
        });

        // Apply all configurations taken from classes that implement IEntityTypeConfiguration<>
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(builder);
    }
}
