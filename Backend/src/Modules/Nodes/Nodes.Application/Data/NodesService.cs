using BuildingBlocks.Application.Data;
using BuildingBlocks.Domain.Nodes.Node.Dtos;
using BuildingBlocks.Domain.Nodes.Node.ValueObjects;
using BuildingBlocks.Domain.Reminders.Reminder.Dtos;
using BuildingBlocks.Domain.Reminders.Reminder.ValueObjects;
using BuildingBlocks.Domain.Timelines.Timeline.ValueObjects;

using Mapster;
using Microsoft.Extensions.DependencyInjection;
using Nodes.Application.Data.Abstractions;
using Nodes.Application.Entities.Nodes.Extensions;

// ReSharper disable NullableWarningSuppressionIsUsed

namespace Nodes.Application.Data;

public class NodesService(IServiceProvider serviceProvider, INodesRepository nodesRepository) : INodesService
{
    #region List
    public async Task<List<NodeDto>> ListNodesPaginated(int pageIndex, int pageSize, CancellationToken cancellationToken)
    {
        var remindersService = serviceProvider.GetRequiredService<IRemindersService>();
        var timelinesService = serviceProvider.GetRequiredService<ITimelinesService>();

        var nodes = await nodesRepository.ListNodesPaginatedAsync(pageIndex, pageSize, cancellationToken);

        var reminders = await remindersService
            .GetRemindersBaseBelongingToNodeIdsAsync(nodes.Select(n => n.Id).ToList(), cancellationToken);

        var timelines = await timelinesService
            .GetTimelinesBaseBelongingToNodeIdsAsync(nodes.Select(n => n.Id).ToList(), cancellationToken);

        var nodeDtos = nodes.Select(n =>
            n.ToNodeDto(
                reminders
                    .Where(r => n.ReminderIds.Select(id => id.ToString()).Contains(r.Id))
                    .Select(r => new ReminderBaseDto(
                        id: r.Id!.ToString(),
                        title: r.Title,
                        description: r.Description,
                        dueDateTime: r.DueDateTime,
                        priority: r.Priority,
                        notificationTime: r.NotificationTime,
                        status: r.Status)
                    )
                    .ToList(),
                timelines
                    .First(t => t.Id == n.TimelineId.ToString())
            )
        ).ToList();

        return nodeDtos;
    }

    public async Task<List<NodeBaseDto>> GetNodesByIdsAsync(IEnumerable<NodeId> nodeIds, CancellationToken cancellationToken)
    {
        return (await nodesRepository.GetNodesByIdsAsync(nodeIds, cancellationToken)).Select(n => n.ToNodeBaseDto()).ToList();
    }

    public async Task<long> CountNodesAsync(CancellationToken cancellationToken)
    {
        return await nodesRepository.NodeCountAsync(cancellationToken);
    }
    #endregion
    
    #region Get
    public async Task<NodeDto> GetNodeByIdAsync(NodeId nodeId, CancellationToken cancellationToken)
    {
        var remindersService = serviceProvider.GetRequiredService<IRemindersService>();
        var timelinesService = serviceProvider.GetRequiredService<ITimelinesService>();

        var node = await nodesRepository.GetNodeByIdAsync(nodeId, cancellationToken);
        
        var reminders = await remindersService
            .GetRemindersBaseBelongingToNodeIdsAsync([node.Id], cancellationToken);

        var timeline = await timelinesService.GetTimelineByIdAsync(node.TimelineId, cancellationToken);

        var nodeDto = node.ToNodeDto(reminders, timeline);

        return nodeDto;
    }

    public async Task<NodeBaseDto> GetNodeBaseByIdAsync(NodeId nodeId, CancellationToken cancellationToken)
    {
        return (await nodesRepository.GetNodeByIdAsync(nodeId, cancellationToken)).ToNodeBaseDto();
    }
    #endregion

    public async Task AddReminder(NodeId nodeId, ReminderId reminderId, CancellationToken cancellationToken)
    {
        var node = await nodesRepository.GetNodeByIdAsync(nodeId, cancellationToken);
        node.AddReminder(reminderId);

        await nodesRepository.UpdateNodeAsync(node, cancellationToken);
    }

    public async Task RemoveNode(NodeId nodeId, CancellationToken cancellationToken)
    {
        var timelinesService = serviceProvider.GetRequiredService<ITimelinesService>();
        var node = await nodesRepository.GetNodeByIdAsync(nodeId, cancellationToken);

        await nodesRepository.RemoveNode(node, cancellationToken);
        await timelinesService.RemoveNode(node.TimelineId, node.Id, cancellationToken);
    }

    #region Relationships
    public async Task<List<NodeBaseDto>> GetNodesBaseBelongingToTimelineIdsAsync(IEnumerable<TimelineId> timelineIds, CancellationToken cancellationToken)
    {
        var nodes = await nodesRepository.GetNodesBelongingToTimelineIdsAsync(timelineIds, cancellationToken);
        var nodeBaseDtos = nodes.Adapt<List<NodeBaseDto>>();
        return nodeBaseDtos;
    }
    #endregion
}
