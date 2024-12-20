using Reminders.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Reminders.Application.Data;

public interface IRemindersDbContext
{
    DbSet<Reminder> Reminders { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
