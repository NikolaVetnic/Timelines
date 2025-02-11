using BuildingBlocks.Application.Data;
using BuildingBlocks.Domain.Nodes.Node.Dtos;
using BuildingBlocks.Domain.Nodes.Node.ValueObjects;
using BuildingBlocks.Domain.Reminders.Reminder.ValueObjects;
using BuildingBlocks.Domain.Timelines.Timeline.Dtos;
using Mapster;
using Microsoft.Extensions.DependencyInjection;
using Nodes.Application.Data.Abstractions;
using Nodes.Application.Entities.Nodes.Extensions;

namespace Nodes.Application.Data;

public class NodesService(INodesRepository nodesRepository, IServiceProvider serviceProvider) : INodesService
{
    public async Task<NodeDto> GetNodeByIdAsync(NodeId nodeId, CancellationToken cancellationToken)
    {
        var node = await nodesRepository.GetNodeByIdAsync(nodeId, cancellationToken);

        var timelineService = serviceProvider.GetRequiredService<ITimelinesService>();
        var timeline = timelineService.GetTimelineByIdAsync(node.TimelineId, cancellationToken);
        var timelineBaseDto = timeline.Adapt<TimelineBaseDto>();

        var nodeDto = node.ToNodeDto(timelineBaseDto);

        var remindersService = serviceProvider.GetRequiredService<IRemindersService>();

        foreach (var reminderId in node.ReminderIds)
        {
            var reminder = await remindersService.GetReminderBaseByIdAsync(reminderId, cancellationToken);
            nodeDto.Reminders.Add(reminder);
        }

        return nodeDto;
    }

    public async Task<NodeBaseDto> GetNodeBaseByIdAsync(NodeId nodeId, CancellationToken cancellationToken)
    {
        var node = await nodesRepository.GetNodeByIdAsync(nodeId, cancellationToken);
        var nodeBaseDto = node.Adapt<NodeBaseDto>();

        return nodeBaseDto;
    }

    public async Task AddReminder(NodeId nodeId, ReminderId reminderId, CancellationToken cancellationToken)
    {
        var node = await nodesRepository.GetNodeByIdAsync(nodeId, cancellationToken);
        node.AddReminder(reminderId);

        await nodesRepository.UpdateNodeAsync(node, cancellationToken);
    }
}
