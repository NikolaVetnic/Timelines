using BuildingBlocks.Domain.ValueObjects.Ids;

namespace Reminders.Domain.Models;

public class Reminder : Aggregate<ReminderId>
{
    public required string Title { get; set; }

    public static Reminder Create(ReminderId id, string title)
    {
        var reminder = new Reminder
        {
            Id = id,
            Title = title
        };

        return reminder;
    }
}
