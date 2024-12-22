namespace Notes.Application.Data;

public interface INotesDbContext
{
    DbSet<Note> Notes { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
