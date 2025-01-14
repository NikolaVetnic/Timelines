using Timelines.Application.Entities.Timelines.Exceptions;
using Timelines.Application.Entities.Timelines.Extensions;

namespace Timelines.Application.Entities.Timelines.Queries.GetTimelineById;

internal class GetTimelineByIdHandler(ITimelinesDbContext dbContext) : IQueryHandler<GetTimelineByIdQuery, GetTimelineByIdResult>
{
    public async Task<GetTimelineByIdResult> Handle(GetTimelineByIdQuery query, CancellationToken cancellationToken)
    {
        var timelineId = query.Id.ToString();

        var timeline = await dbContext.Timelines
            .AsNoTracking()
            .SingleOrDefaultAsync(t => t.Id == TimelineId.Of(Guid.Parse(timelineId)), cancellationToken);

        if (timeline is null)
            throw new TimelineNotFoundException(timelineId);

        return new GetTimelineByIdResult(timeline.ToTimelineDto());
    }
}
