using System.Reflection;
using BugTracking.Application.Data;
using BugTracking.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace BugTracking.Infrastructure.Data;

public class BugTrackingDbContext(DbContextOptions<BugTrackingDbContext> options) :
    DbContext(options), IBugTrackingDbContext
{
    public DbSet<BugReport> BugReports { get; init; }
    
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        // Specify schema for this module
        builder.HasDefaultSchema("BugReports");

        // Configure entities
        builder.Entity<BugReport>(entity =>
        {
            entity.ToTable("BugReports"); // Specify table name within the schema
        });

        // Apply all configurations taken from classes that implement IEntityTypeConfiguration<>
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(builder);
    }
}
