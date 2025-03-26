using BuildingBlocks.Application.Data;
using Timelines.Application.Entities.Timelines.Exceptions;

namespace Timelines.Application.Entities.Timelines.Commands.DeleteTimeline;

public class DeleteTimelineHandler(ITimelinesService timelinesService) : ICommandHandler<DeleteTimelineCommand, DeleteTimelineResult>
{
    public async Task<DeleteTimelineResult> Handle(DeleteTimelineCommand command, CancellationToken cancellationToken)
    {
        var timeline = await timelinesService.GetTimelineBaseByIdAsync(command.Id, cancellationToken);

        if (timeline is null)
            throw new TimelineNotFoundException(command.Id.ToString());

        await timelinesService.RemoveTimeline(command.Id, cancellationToken);

        return new DeleteTimelineResult(true);
    }
}
