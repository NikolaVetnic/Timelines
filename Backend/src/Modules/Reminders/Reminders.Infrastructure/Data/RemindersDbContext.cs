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
            entity.Property(r => r.DueDateTime).IsRequired();
            entity.Property(r => r.Priority).IsRequired();
            entity.Property(r => r.NotificationTime).IsRequired();
            entity.Property(r => r.Status).IsRequired();

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
