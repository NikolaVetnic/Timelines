using BuildingBlocks.Domain.ValueObjects.Ids;
using Timelines.Application.Data;

namespace Timelines.Application.Timelines.Commands.CreateTimeline;

public class CreateTimelineHandler(ITimelinesDbContext dbContext) :
    ICommandHandler<CreateTimelineCommand, CreateTimelineResult>
{
    public async Task<CreateTimelineResult> Handle(CreateTimelineCommand command, CancellationToken cancellationToken)
    {
        var timeline = Timeline.Create(
            TimelineId.Of(Guid.NewGuid()),
            command.Timeline.Title
        );

        dbContext.Timelines.Add(timeline);
        await dbContext.SaveChangesAsync(cancellationToken);

        return new CreateTimelineResult(timeline.Id);
    }
}
