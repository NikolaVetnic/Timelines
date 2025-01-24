using Timelines.Domain.Events;

namespace Timelines.Domain.Models;

public class Timeline : Aggregate<TimelineId>
{
    public required string Title { get; set; }
    public required string Description { get; set; }

    #region Timeline

    public static Timeline Create(TimelineId id, string title, string description)
    {
        var timeline = new Timeline
        {
            Id = id,
            Title = title,
            Description = description
        };

        timeline.AddDomainEvent(new TimelineCreatedEvent(timeline));

        return timeline;
    }

    public void Update(string title, string description)
    {
        Title = title;
        Description = description;

        AddDomainEvent(new TimelineUpdatedEvent(this));
    }

    #endregion
}
