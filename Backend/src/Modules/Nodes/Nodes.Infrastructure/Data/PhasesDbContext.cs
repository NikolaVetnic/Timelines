using Nodes.Application.Data.Abstractions;
using System.Reflection;

namespace Nodes.Infrastructure.Data;

public class PhasesDbContext(DbContextOptions<PhasesDbContext> options) : DbContext(options), IPhasesDbContext
{
    public DbSet<Phase> Phases { get; init; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        // Specify schema for this module
        builder.HasDefaultSchema("Phases");

        // Configure entities
        builder.Entity<Phase>(entity =>
        {
            entity.ToTable("Phases"); // Specify table name within the schema

            entity.HasKey(n => n.Id);
            entity.Property(n => n.Title).IsRequired();
            entity.Property(n => n.Description).IsRequired();
            entity.Property(n => n.StartDate).IsRequired();
            entity.Property(n => n.Status).IsRequired();
            entity.Property(n => n.Progress).IsRequired();
            entity.Property(n => n.IsCompleted).IsRequired();
            entity.Property(n => n.Parent).IsRequired();
            entity.Property(n => n.DependsOn).IsRequired();
            entity.Property(n => n.AssignedTo).IsRequired();
            entity.Property(n => n.Stakeholders).IsRequired();
            entity.Property(n => n.Tags).IsRequired();

            // Map the ReminderIds as a collection of IDs
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