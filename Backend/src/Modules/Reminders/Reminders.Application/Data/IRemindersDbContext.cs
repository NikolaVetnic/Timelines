using Reminders.Domain.Models;

namespace Reminders.Application.Data;
using Microsoft.EntityFrameworkCore;

public interface IRemindersDbContext
{
    DbSet<Reminder> Reminder { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
