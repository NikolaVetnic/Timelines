﻿using BuildingBlocks.Domain.Nodes.Node.Dtos;
using BuildingBlocks.Domain.Timelines.Phase.Dtos;
using BuildingBlocks.Domain.Timelines.Timeline.Dtos;

namespace Timelines.Application.Entities.Timelines.Extensions;

public static class TimelineExtensions
{
    public static TimelineDto ToTimelineDto(this Timeline timeline)
    {
        return new TimelineDto(
            timeline.Id.ToString(),
            timeline.Title,
            timeline.Description,
            timeline.OwnerId);
    }

    public static TimelineDto ToTimelineDto(this Timeline timeline, IEnumerable<NodeBaseDto> nodes, IEnumerable<PhaseBaseDto> phases)
    {
        return new TimelineDto(
            timeline.Id.ToString(),
            timeline.Title,
            timeline.Description,
            timeline.OwnerId)
        {
            Nodes = nodes.ToList(),
            Phases = phases.ToList()
        };
    }

    public static IEnumerable<TimelineDto> ToTimelineDtoList(this IEnumerable<Timeline> timelines)
    {
        return timelines.Select(ToTimelineDto);
    }

    public static TimelineDto ToTimelineDtoWith(this Timeline timeline, List<NodeBaseDto> nodes, List<PhaseBaseDto> phases)
    {
        var dto = timeline.ToTimelineDto();
        dto.Nodes.AddRange(nodes);
        dto.Phases.AddRange(phases);
        return dto;
    }
}
