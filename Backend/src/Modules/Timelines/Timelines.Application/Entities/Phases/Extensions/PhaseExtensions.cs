using BuildingBlocks.Domain.Timelines.Phase.Dtos;

namespace Timelines.Application.Entities.Phases.Extensions;

public static class PhaseExtensions
{
    public static PhaseBaseDto ToPhaseBaseDto(this Phase phase)
    {
        return new PhaseBaseDto(
            phase.Id.ToString(),
            phase.Title,
            phase.Description,
            phase.StartDate,
            phase.EndDate,
            phase.Duration,
            phase.Status,
            phase.Progress,
            phase.IsCompleted,
            phase.Parent,
            phase.DependsOn,
            phase.AssignedTo,
            phase.Stakeholders,
            phase.Tags);
    }

}
