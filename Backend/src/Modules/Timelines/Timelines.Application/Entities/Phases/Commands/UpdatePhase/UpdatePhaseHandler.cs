using BuildingBlocks.Application.Data;
using BuildingBlocks.Domain.Timelines.Timeline.ValueObjects;
using Timelines.Application.Data.Abstractions;
using Timelines.Application.Entities.Phases.Exceptions;
using Timelines.Application.Entities.Phases.Extensions;

namespace Timelines.Application.Entities.Phases.Commands.UpdatePhase;

internal class UpdatePhaseHandler(IPhasesRepository phasesRepository, ITimelinesService timelinesService)
    : ICommandHandler<UpdatePhaseCommand, UpdatePhaseResult>
{
    public async Task<UpdatePhaseResult> Handle(UpdatePhaseCommand command, CancellationToken cancellationToken)
    {
        var phase = await phasesRepository.GetPhaseByIdAsync(command.Id, cancellationToken);

        if (phase is null)
            throw new PhaseNotFoundException(command.Id.ToString());

        phase.Title = command.Title ?? phase.Title;
        phase.Description = command.Description ?? phase.Description;
        phase.StartDate = command.StartDate ?? phase.StartDate;
        phase.EndDate = command.EndDate;
        phase.Duration = command.Duration ?? phase.Duration;
        phase.Status = command.Status ?? phase.Status;
        phase.Progress = command.Progress ?? phase.Progress;
        phase.IsCompleted = command.IsCompleted ?? phase.IsCompleted;
        phase.Parent = command.Parent ?? phase.Parent;
        phase.DependsOn = command.DependsOn.ToList() ?? phase.DependsOn;
        phase.AssignedTo = command.AssignedTo ?? phase.AssignedTo;
        phase.Stakeholders = command.Stakeholders.ToList() ?? phase.Stakeholders;
        phase.Tags = command.Tags.ToList() ?? phase.Tags;

        var timeline = await timelinesService.GetTimelineByIdAsync(command.TimelineId ?? phase.TimelineId, cancellationToken);

        if (timeline.Id is null)
            throw new NotFoundException(
                $"Related timeline with ID {command.TimelineId ?? phase.TimelineId} not found");

        phase.TimelineId = TimelineId.Of(Guid.Parse(timeline.Id));

        await phasesRepository.UpdatePhaseAsync(phase, cancellationToken);

        return new UpdatePhaseResult(phase.ToPhaseBaseDto());
    }
}
