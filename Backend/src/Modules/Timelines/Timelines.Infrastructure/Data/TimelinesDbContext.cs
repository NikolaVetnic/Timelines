using Microsoft.EntityFrameworkCore;
using System.Reflection;
using Timelines.Application.Data;
using Timelines.Domain.Models;

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
        });

        // Apply all configurations taken from classes that implement IEntityTypeConfiguration<>
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(builder);
    }
}

