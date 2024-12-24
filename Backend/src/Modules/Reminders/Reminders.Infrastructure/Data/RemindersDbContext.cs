using System.Reflection;
using Reminders.Application.Data;

namespace Reminders.Infrastructure.Data;

public class RemindersDbContext(DbContextOptions<RemindersDbContext> options) :
    DbContext(options), IRemindersDbContext
{
    public DbSet<Reminder> Reminders { get; init; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        // Specify schema for this module
        builder.HasDefaultSchema("Reminders");

        // Configure entities
        builder.Entity<Reminder>(entity =>
        {
            entity.ToTable("Reminders"); // Specify table name within the schema
        });

        // Apply all configurations taken from classes that implement IEntityTypeConfiguration<>
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(builder);
    }
}
