namespace Notes.Application.Data.Abstractions;

public interface INotesDbContext
{
    DbSet<Note> Notes { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
