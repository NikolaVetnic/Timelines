using Timelines.Application.Data.Abstractions;
using Timelines.Application.Entities.Timelines.Exceptions;

namespace Timelines.Application.Entities.Timelines.Commands.ReviveTimeline;

internal class ReviveTimelineHandler(ITimelinesRepository timelinesRepository)
: ICommandHandler<ReviveTimelineCommand, ReviveTimelineResponse>
{
    public async Task<ReviveTimelineResponse> Handle(ReviveTimelineCommand command, CancellationToken cancellationToken)
    {
        var timeline = await timelinesRepository.GetTimelineByIdAsync(command.Id, cancellationToken);

        if (timeline is null)
            throw new TimelineNotFoundException(command.Id.ToString());

        await timelinesRepository.ReviveTimeline(timeline.Id, cancellationToken);

        return new ReviveTimelineResponse(true);
    }
}
