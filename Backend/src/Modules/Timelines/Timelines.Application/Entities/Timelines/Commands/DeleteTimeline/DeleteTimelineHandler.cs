using BuildingBlocks.Application.Data;
using Timelines.Application.Data.Abstractions;
using Timelines.Application.Entities.Timelines.Exceptions;

namespace Timelines.Application.Entities.Timelines.Commands.DeleteTimeline;

public class DeleteTimelineHandler(INodesService nodesService, 
    IPhasesService phasesService, ITimelinesRepository timelinesRepository) : ICommandHandler<DeleteTimelineCommand, DeleteTimelineResult>
{
    public async Task<DeleteTimelineResult> Handle(DeleteTimelineCommand command, CancellationToken cancellationToken)
    {
        var timeline = await timelinesRepository.GetTimelineByIdAsync(command.Id, cancellationToken);
        
        if (timeline is null)
            throw new TimelineNotFoundException(command.Id.ToString());
        
        await nodesService.DeleteNodes(timeline.Id, timeline.NodeIds, cancellationToken);
        await phasesService.DeletePhases(timeline.Id, timeline.PhaseIds, cancellationToken);
        await timelinesRepository.DeleteTimeline(timeline.Id, cancellationToken);

        return new DeleteTimelineResult(true);
    }
}
