using BuildingBlocks.Domain.Nodes.Node.ValueObjects;
using BuildingBlocks.Domain.Nodes.Phase.Dtos;
using BuildingBlocks.Domain.Nodes.Phase.ValueObjects;
using BuildingBlocks.Domain.Timelines.Timeline.Dtos;

namespace BuildingBlocks.Application.Data;

public interface IPhasesService
{
    Task<PhaseBaseDto> GetPhaseBaseByIdAsync(PhaseId phaseId, CancellationToken cancellationToken);

    Task<List<PhaseBaseDto>> GetPhasesBaseBelongingToNodeIdsAsync(IEnumerable<NodeId> nodeIds, CancellationToken cancellationToken);
}
