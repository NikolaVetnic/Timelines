using System.Reflection;
using Nodes.Application.Data.Abstractions.Phases;

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

            entity.HasKey(p => p.Id);
            entity.Property(p => p.Title).IsRequired();
            entity.Property(p => p.Description).IsRequired();
            entity.Property(p => p.StartDate).IsRequired();
            entity.Property(p => p.EndDate).IsRequired(false);
            entity.Property(p => p.Duration).IsRequired(false);
            entity.Property(p => p.Status).IsRequired();
            entity.Property(p => p.Progress).IsRequired();
            entity.Property(p => p.IsCompleted).IsRequired();
            entity.Property(p => p.Parent).IsRequired();

            //TODO: this is not finished yet

        });

        // Apply all configurations taken from classes that implement IEntityTypeConfiguration<>
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(builder);
    }
}
