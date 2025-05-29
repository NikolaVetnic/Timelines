using BuildingBlocks.Domain.Timelines.Phase.Dtos;
using BuildingBlocks.Domain.Timelines.Phase.ValueObjects;
using BuildingBlocks.Domain.Timelines.Timeline.ValueObjects;

namespace BuildingBlocks.Application.Data;

public interface IPhasesService
{
    Task<PhaseBaseDto> GetPhaseBaseByIdAsync(PhaseId phaseId, CancellationToken cancellationToken);
    Task<PhaseDto> GetPhaseByIdAsync(PhaseId phaseId, CancellationToken cancellationToken);
    Task<List<PhaseBaseDto>> GetPhasesByIdsAsync(IEnumerable<PhaseId> phaseIds, CancellationToken cancellationToken);
    Task<List<PhaseBaseDto>> GetPhasesBaseBelongingToTimelineIdsAsync(IEnumerable<TimelineId> timelineIds, CancellationToken cancellationToken);


    Task DeletePhase(PhaseId phaseId, CancellationToken cancellationToken);
    Task DeletePhases(TimelineId timelineId, IEnumerable<PhaseId> phaseIds, CancellationToken cancellationToken);
}
