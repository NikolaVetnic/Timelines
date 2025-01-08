// ReSharper disable ClassNeverInstantiated.Global

namespace Timelines.Application.Entities.Timelines.Queries.GetTimelineById;

public record GetTimelineByIdQuery(string Id) : IQuery<GetTimelineByIdResult>;

// ReSharper disable once NotAccessedPositionalProperty.Global
public record GetTimelineByIdResult(TimelineDto TimelineDto);
