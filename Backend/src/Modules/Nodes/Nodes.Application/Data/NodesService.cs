using BuildingBlocks.Application.Data;
using BuildingBlocks.Domain.Nodes.Node.Dtos;
using BuildingBlocks.Domain.Nodes.Node.ValueObjects;
using BuildingBlocks.Domain.Reminders.Reminder.Dtos;
using BuildingBlocks.Domain.Reminders.Reminder.ValueObjects;
using Mapster;
using Nodes.Application.Data.Abstractions;
using Nodes.Application.Entities.Nodes.Extensions;

// ReSharper disable NullableWarningSuppressionIsUsed

namespace Nodes.Application.Data;

public class NodesService(INodesRepository nodesRepository, IRemindersService remindersService) : INodesService
{
    public async Task<List<NodeDto>> ListNodesPaginated(int pageIndex, int pageSize,
        CancellationToken cancellationToken)
    {
        var nodes = await nodesRepository.ListNodesPaginatedAsync(pageIndex, pageSize, cancellationToken);
        
        var reminders = await remindersService
            .GetRemindersBaseBelongingToNodeIdsAsync(nodes.Select(n => n.Id).ToList(), cancellationToken);

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
                        .ToList()
                ))
            .ToList();

        return nodeDtos;
    }

    public async Task<NodeDto> GetNodeByIdAsync(NodeId nodeId, CancellationToken cancellationToken)
    {
        var node = await nodesRepository.GetNodeByIdAsync(nodeId, cancellationToken);
        var nodeDto = node.ToNodeDto();

        foreach (var reminderId in node.ReminderIds)
        {
            var reminder = await remindersService.GetReminderBaseByIdAsync(reminderId, cancellationToken);
            nodeDto.Reminders.Add(reminder);
        }

        var notesService = serviceProvider.GetRequiredService<INotesService>();

        foreach (var noteId in node.NoteIds)
        {
            var note = await notesService.GetNoteBaseByIdAsync(noteId, cancellationToken);
            nodeDto.Notes.Add(note);
        }

        return nodeDto;
    }

    public async Task<NodeBaseDto> GetNodeBaseByIdAsync(NodeId nodeId, CancellationToken cancellationToken)
    {
        var node = await nodesRepository.GetNodeByIdAsync(nodeId, cancellationToken);
        var nodeBaseDto = node.Adapt<NodeBaseDto>();

        return nodeBaseDto;
    }

    public async Task<long> CountNodesAsync(CancellationToken cancellationToken)
    {
        return await nodesRepository.NodeCountAsync(cancellationToken);
    }

    public async Task AddReminder(NodeId nodeId, ReminderId reminderId, CancellationToken cancellationToken)
    {
        var node = await nodesRepository.GetNodeByIdAsync(nodeId, cancellationToken);
        node.AddReminder(reminderId);

        await nodesRepository.UpdateNodeAsync(node, cancellationToken);
    }

    public async Task AddNote(NodeId nodeId, NoteId noteId, CancellationToken cancellationToken)
    {
        var node = await nodesRepository.GetNodeByIdAsync(nodeId, cancellationToken);
        node.AddNote(noteId);

        await nodesRepository.UpdateNodeAsync(node, cancellationToken);
    }

    public async Task RemoveNote(NodeId nodeId, NoteId noteId, CancellationToken cancellationToken)
    {
        var node = await nodesRepository.GetNodeByIdAsync(nodeId, cancellationToken);
        node.RemoveNote(noteId);

        await nodesRepository.UpdateNodeAsync(node, cancellationToken);
    }
}
