using BuildingBlocks.Application.Data;
using Timelines.Application.Entities.Timelines.Exceptions;

namespace Timelines.Application.Entities.Timelines.Queries.GetTimelineById;

internal class GetTimelineByIdHandler(ITimelinesService timelinesService)
    : IQueryHandler<GetTimelineByIdQuery, GetTimelineByIdResult>
{
    public async Task<GetTimelineByIdResult> Handle(GetTimelineByIdQuery query, CancellationToken cancellationToken)
    {
        var timelineDto = await timelinesService.GetTimelineByIdAsync(query.Id, cancellationToken);

        if (timelineDto is null)
            throw new TimelineNotFoundException(query.Id.ToString());

        return new GetTimelineByIdResult(timelineDto);
    }
}
