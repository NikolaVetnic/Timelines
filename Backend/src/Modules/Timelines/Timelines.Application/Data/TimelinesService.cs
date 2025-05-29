using BuildingBlocks.Application.Data;
using BuildingBlocks.Domain.Nodes.Node.Dtos;
using BuildingBlocks.Domain.Nodes.Node.ValueObjects;
using BuildingBlocks.Domain.Timelines.Phase.Dtos;
using BuildingBlocks.Domain.Timelines.Phase.ValueObjects;
using BuildingBlocks.Domain.Timelines.Timeline.Dtos;
using BuildingBlocks.Domain.Timelines.Timeline.ValueObjects;
using Mapster;
using Microsoft.Extensions.DependencyInjection;
using Timelines.Application.Data.Abstractions;
using Timelines.Application.Entities.Timelines.Extensions;

namespace Timelines.Application.Data;

public class TimelinesService(IServiceProvider serviceProvider, ITimelinesRepository timelinesRepository) : ITimelinesService
{
    private INodesService NodesService => serviceProvider.GetRequiredService<INodesService>();
    private IPhasesService PhasesService => serviceProvider.GetRequiredService<IPhasesService>();

    #region List

    public async Task<List<TimelineDto>> ListTimelinesPaginated(int pageIndex, int pageSize, CancellationToken cancellationToken)
    {

        var timelines = await timelinesRepository.ListTimelinesPaginatedAsync(pageIndex, pageSize, cancellationToken);

        var nodes = await NodesService.GetNodesBaseBelongingToTimelineIdsAsync(timelines.Select(t => t.Id),
            cancellationToken);

        var phases = await PhasesService.GetPhasesBaseBelongingToTimelineIdsAsync(timelines.Select(t => t.Id),
            cancellationToken);

        var timelineDtos = timelines.Select(t =>
                t.ToTimelineDto(
                    nodes
                        .Where(n => t.NodeIds.Select(id => id.ToString()).Contains(n.Id))
                        .Select(n => new NodeBaseDto(
                            id: n.Id!.ToString(),
                            title: n.Title,
                            description: n.Description,
                            timestamp: n.Timestamp,
                            importance: n.Importance,
                            categories: n.Categories,
                            tags: n.Tags)
                        )
                        .ToList(),
                    phases
                        .Where(p => t.PhaseIds.Select(id => id.ToString()).Contains(p.Id))
                        .Select(p => new PhaseBaseDto(
                            id: p.Id!.ToString(),
                            title: p.Title,
                            description: p.Description,
                            startDate: p.StartDate,
                            endDate: p.EndDate,
                            duration: p.Duration,
                            status: p.Status,
                            progress: p.Progress,
                            isCompleted: p.IsCompleted,
                            parent: p.Parent,
                            dependsOn: p.DependsOn,
                            assignedTo: p.AssignedTo,
                            stakeholders: p.Stakeholders,
                            tags: p.Tags
                            )
                        )
                        .ToList()

                ))
            .ToList();
        return timelineDtos;
    }

    public async Task<List<TimelineDto>> ListFlaggedForDeletionTimelinesPaginated(int pageIndex, int pageSize, CancellationToken cancellationToken)
    {

        var timelines = await timelinesRepository.ListFlaggedForDeletionTimelinesPaginatedAsync(pageIndex, pageSize, cancellationToken);

        var nodes = await NodesService.GetNodesBaseBelongingToTimelineIdsAsync(timelines.Select(t => t.Id),
            cancellationToken);

        var phases = await PhasesService.GetPhasesBaseBelongingToTimelineIdsAsync(timelines.Select(t => t.Id),
            cancellationToken);

        var timelineDtos = timelines.Select(t =>
                t.ToTimelineDto(
                    nodes
                        .Where(n => t.NodeIds.Select(id => id.ToString()).Contains(n.Id))
                        .Select(n => new NodeBaseDto(
                            id: n.Id!.ToString(),
                            title: n.Title,
                            description: n.Description,
                            timestamp: n.Timestamp,
                            importance: n.Importance,
                            categories: n.Categories,
                            tags: n.Tags)
                        )
                        .ToList(),
                    phases
                        .Where(p => t.PhaseIds.Select(id => id.ToString()).Contains(p.Id))
                        .Select(p => new PhaseBaseDto(
                            id: p.Id!.ToString(),
                            title: p.Title,
                            description: p.Description,
                            startDate: p.StartDate,
                            endDate: p.EndDate,
                            duration: p.Duration,
                            status: p.Status,
                            progress: p.Progress,
                            isCompleted: p.IsCompleted,
                            parent: p.Parent,
                            dependsOn: p.DependsOn,
                            assignedTo: p.AssignedTo,
                            stakeholders: p.Stakeholders,
                            tags: p.Tags
                            )
                        )
                        .ToList()

                ))
            .ToList();
        return timelineDtos;
    }

    public async Task<long> CountTimelinesAsync(CancellationToken cancellationToken)
    {
        return await timelinesRepository.TimelineCountAsync(cancellationToken);
    }

    public async Task EnsureTimelineBelongsToOwner(TimelineId timelineId, CancellationToken cancellationToken)
    {
        await timelinesRepository.GetTimelineByIdAsync(timelineId, cancellationToken);
    }

    #endregion

    #region Get

    public async Task<TimelineDto> GetTimelineByIdAsync(TimelineId timelineId, CancellationToken cancellationToken)
    {
        var timeline = await timelinesRepository.GetTimelineByIdAsync(timelineId, cancellationToken);

        var nodes = await NodesService.GetNodesByIdsAsync(timeline.NodeIds, cancellationToken);
        var phases = await PhasesService.GetPhasesByIdsAsync(timeline.PhaseIds, cancellationToken);

        return timeline.ToTimelineDtoWith(nodes, phases);
    }
    public async Task<TimelineBaseDto> GetTimelineBaseByIdAsync(TimelineId timelineId,
        CancellationToken cancellationToken)
    {
        var timeline = await timelinesRepository.GetTimelineByIdAsync(timelineId, cancellationToken);
        var timelineBaseDto = timeline.Adapt<TimelineBaseDto>();
        return timelineBaseDto;
    }
    #endregion

    public async Task AddNode(TimelineId timelineId, NodeId nodeId, CancellationToken cancellationToken)
    {
        var timeline = await timelinesRepository.GetTimelineByIdAsync(timelineId, cancellationToken);
        timeline.AddNode(nodeId);
        await timelinesRepository.UpdateTimelineAsync(timeline, cancellationToken);
    }
    public async Task RemoveNode(TimelineId timelineId, NodeId nodeId, CancellationToken cancellationToken)
    {
        await RemoveNodes(timelineId, [nodeId], cancellationToken);
    }

    public async Task RemoveNodes(TimelineId timelineId, IEnumerable<NodeId> nodeIds, CancellationToken cancellationToken)
    {
        var timeline = await timelinesRepository.GetTrackedTimelineByIdAsync(timelineId, cancellationToken);

        foreach (var nodeId in nodeIds)
            timeline.RemoveNode(nodeId);
        await timelinesRepository.UpdateTimelineAsync(timeline, cancellationToken);
    }

    public async Task AddPhase(TimelineId timelineId, PhaseId phaseId, CancellationToken cancellationToken)
    {
        var timeline = await timelinesRepository.GetTimelineByIdAsync(timelineId, cancellationToken);
        timeline.AddPhase(phaseId);
        await timelinesRepository.UpdateTimelineAsync(timeline, cancellationToken);
    }

    public async Task RemovePhase(TimelineId timelineId, PhaseId phaseId, CancellationToken cancellationToken)
    {
        var timeline = await timelinesRepository.GetTimelineByIdAsync(timelineId, cancellationToken);
        timeline.RemovePhase(phaseId);
        await timelinesRepository.UpdateTimelineAsync(timeline, cancellationToken);
    }

    public async Task RemovePhases(TimelineId timelineId, IEnumerable<PhaseId> phaseIds, CancellationToken cancellationToken)
    {
        var timeline = await timelinesRepository.GetTrackedTimelineByIdAsync(timelineId, cancellationToken);

        foreach (var phaseId in phaseIds)
            timeline.RemovePhase(phaseId);
        await timelinesRepository.UpdateTimelineAsync(timeline, cancellationToken);
    }

    #region Relationships

    public async Task<List<TimelineBaseDto>> GetTimelinesBaseBelongingToNodeIdsAsync(IEnumerable<NodeId> nodeIds, CancellationToken cancellationToken)
    {
        var timelines = await timelinesRepository.GetTimelinesBelongingToNodeIdsAsync(nodeIds, cancellationToken);
        var timelineBaseDtos = timelines.Adapt<List<TimelineBaseDto>>();
        return timelineBaseDtos;
    }

    #endregion
}
