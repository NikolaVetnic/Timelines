using BuildingBlocks.Domain.Timelines.Timeline.ValueObjects;

namespace Timelines.Application.Data.Abstractions;

public interface ITimelinesRepository
{
    Task<Timeline> GetTimelineByIdAsync(TimelineId timelineId, CancellationToken cancellationToken);
    Task UpdateTimelineAsync(Timeline timeline, CancellationToken cancellationToken);
}
