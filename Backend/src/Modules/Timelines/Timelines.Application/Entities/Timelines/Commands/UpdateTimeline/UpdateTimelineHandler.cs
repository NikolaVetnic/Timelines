using Timelines.Application.Data.Abstractions;
using Timelines.Application.Entities.Timelines.Exceptions;
using Timelines.Application.Entities.Timelines.Extensions;

namespace Timelines.Application.Entities.Timelines.Commands.UpdateTimeline;

internal class UpdateTimelineHandler(ITimelinesRepository timelinesRepository)
    : ICommandHandler<UpdateTimelineCommand, UpdateTimelineResult>
{
    public async Task<UpdateTimelineResult> Handle(UpdateTimelineCommand command, CancellationToken cancellationToken)
    {
        var timeline = await timelinesRepository.GetTimelineByIdAsync(command.Id, cancellationToken);

        if (timeline is null)
            throw new TimelineNotFoundException(command.Id.ToString());
        
        timeline.Title = command.Title ?? timeline.Title;
        timeline.Description = command.Description ?? timeline.Description;

        await timelinesRepository.UpdateTimelineAsync(timeline, cancellationToken);
        
        return new UpdateTimelineResult(timeline.ToTimelineDto());
    }
}
