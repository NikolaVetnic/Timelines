namespace Reminders.Application.Data.Abstractions;

public interface IRemindersDbContext
{
    DbSet<Reminder> Reminders { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
