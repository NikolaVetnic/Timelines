namespace Reminders.Application.Data;

public interface IRemindersDbContext
{
    DbSet<Reminder> Reminders { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
