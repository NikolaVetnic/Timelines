// ReSharper disable ClassNeverInstantiated.Global

using Timelines.Application.Entities.Timelines.Dtos;

namespace Timelines.Application.Entities.Timelines.Queries.GetTimelineById;

public record GetTimelineByIdQuery(string Id) : IQuery<GetTimelineByIdResult>;

// ReSharper disable once NotAccessedPositionalProperty.Global
public record GetTimelineByIdResult(TimelineDto TimelineDto);
