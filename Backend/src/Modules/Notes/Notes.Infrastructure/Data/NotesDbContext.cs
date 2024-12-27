using Notes.Application.Data;
using System.Reflection;

namespace Notes.Infrastructure.Data;

public class NotesDbContext(DbContextOptions<NotesDbContext> options)
    : DbContext(options), INotesDbContext
{
    public DbSet<Note> Notes { get; init; } = null!;

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        // Specify schema for this module
        builder.HasDefaultSchema("Notes");

        // Configure entities
        builder.Entity<Note>(entity =>
        {
            entity.ToTable("Notes"); // Specify table name within the schema
        });

        // Apply all configurations taken from classes that implement IEntityTypeConfiguration<>
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(builder);
    }
}
