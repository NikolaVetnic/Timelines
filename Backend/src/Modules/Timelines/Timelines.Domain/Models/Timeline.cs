using BuildingBlocks.Domain.Timelines.Timeline.Events;

namespace Timelines.Domain.Models;

public class Timeline : Aggregate<TimelineId>
{
    public required string Title { get; set; }

    #region Timeline

    public static Timeline Create(TimelineId id, string title)
    {
        var timeline = new Timeline
        {
            Id = id,
            Title = title
        };

        timeline.AddDomainEvent(new TimelineCreatedEvent(timeline.Id));

        return timeline;
    }

    public void Update(string title)
    {
        Title = title;

        AddDomainEvent(new TimelineUpdatedEvent(Id));
    }

    #endregion
}
