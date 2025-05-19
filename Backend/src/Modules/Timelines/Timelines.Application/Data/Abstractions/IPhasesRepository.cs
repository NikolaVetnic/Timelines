using BuildingBlocks.Domain.Nodes.Node.ValueObjects;
using BuildingBlocks.Domain.Timelines.Phase.ValueObjects;
using BuildingBlocks.Domain.Timelines.Timeline.ValueObjects;

namespace Timelines.Application.Data.Abstractions;

public interface IPhasesRepository
{
    Task CreatePhaseAsync(Phase phase, CancellationToken cancellationToken);

    Task<List<Phase>> ListPhasesPaginatedAsync(int pageIndex, int pageSize, CancellationToken cancellationToken);
    Task<Phase> GetPhaseByIdAsync(PhaseId phaseId, CancellationToken cancellationToken);
    Task<IEnumerable<Phase>> GetPhasesBelongingToNodeIdsAsync(IEnumerable<NodeId> nodeIds, CancellationToken cancellationToken);
    Task<List<Phase>> GetPhasesByIdsAsync(IEnumerable<PhaseId> phaseIds, CancellationToken cancellationToken);

    Task<IEnumerable<Phase>> GetPhasesBelongingToTimelineIdsAsync(IEnumerable<TimelineId> timelineIds, CancellationToken cancellationToken);
    Task<long> PhaseCountAsync(CancellationToken cancellationToken);

    Task UpdatePhaseAsync(Phase phase, CancellationToken cancellationToken);
    Task DeletePhaseAsync(PhaseId phaseId, CancellationToken cancellationToken);
    Task DeletePhases(IEnumerable<PhaseId> phaseIds, CancellationToken cancellationToken);


}
