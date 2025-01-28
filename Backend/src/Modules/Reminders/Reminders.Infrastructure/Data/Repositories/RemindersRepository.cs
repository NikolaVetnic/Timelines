using BuildingBlocks.Domain.Reminders.ValueObjects;
using Reminders.Application.Data.Abstractions;
using Reminders.Application.Entities.Reminders.Exceptions;

namespace Reminders.Infrastructure.Data.Repositories;

public class RemindersRepository(IRemindersDbContext dbContext) : IRemindersRepository
{
    public async Task<Reminder> GetReminderByIdAsync(ReminderId reminderId, CancellationToken cancellationToken)
    {
        return await dbContext.Reminders
                   .AsNoTracking()
                   .SingleOrDefaultAsync(r => r.Id == reminderId, cancellationToken) ??
               throw new ReminderNotFoundException(reminderId.ToString());
    }
}
