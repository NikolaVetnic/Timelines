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

            entity.HasKey(n => n.Id);
            entity.Property(n => n.Title).IsRequired();
            entity.Property(n => n.Content).IsRequired();
            entity.Property(n => n.Timestamp).IsRequired();
            entity.Property(n => n.Owner).IsRequired();
            entity.Property(n => n.IsPublic).IsRequired();

            // Map the RelatedNotes as a collection of IDs
            entity.Ignore(n => n.RelatedNotes); // This prevents EF from expecting a navigation property
            entity.Property(n => n.Id).ValueGeneratedNever(); // Ensures IDs are managed externally

            entity.Property(e => e.RelatedNotes)
                .HasConversion(new RelatedNoteIdListConverter())
                .HasColumnName("RelatedNotes")
                .IsRequired(false);

            entity.Property(e => e.SharedWith)
                .HasConversion(new StringListConverter())
                .HasColumnName("SharedWith")
                .IsRequired(false);

        });

        // Apply all configurations taken from classes that implement IEntityTypeConfiguration<>
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(builder);
    }
}
