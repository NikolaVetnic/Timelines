using Timelines.Application.Entities.Timelines.Dtos;

namespace Timelines.Application.Entities.Timelines.Extensions;

public static class TimelineExtensions
{
    public static TimelineDto ToTimelineDto(this Timeline timeline)
    {
        return new TimelineDto(
            timeline.Id.ToString(),
            timeline.Title,
            timeline.Description);
    }

    public static IEnumerable<TimelineDto> ToTimelineDtoList(this IEnumerable<Timeline> timelines)
    {
        return timelines.Select(ToTimelineDto);
    }
}
