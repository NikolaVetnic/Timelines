using Timelines.Application.Data.Abstractions;
using Timelines.Application.Entities.Timelines.Exceptions;
using Timelines.Application.Entities.Timelines.Extensions;

namespace Timelines.Application.Entities.Timelines.Queries.GetTimelineById;

internal class GetTimelineByIdHandler(ITimelinesDbContext dbContext) : IQueryHandler<GetTimelineByIdQuery, GetTimelineByIdResult>
{
    public async Task<GetTimelineByIdResult> Handle(GetTimelineByIdQuery query, CancellationToken cancellationToken)
    {
        var timeline = await dbContext.Timelines
            .AsNoTracking()
            .SingleOrDefaultAsync(t => t.Id == query.Id, cancellationToken);

        if (timeline is null)
            throw new TimelineNotFoundException(query.Id.ToString());

        return new GetTimelineByIdResult(timeline.ToTimelineDto());
    }
}
