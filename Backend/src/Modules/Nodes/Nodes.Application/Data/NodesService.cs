using BuildingBlocks.Application.Data;
using BuildingBlocks.Domain.Nodes.Node.Dtos;
using BuildingBlocks.Domain.Nodes.Node.ValueObjects;
using BuildingBlocks.Domain.Notes.Note.Dtos;
using BuildingBlocks.Domain.Notes.Note.ValueObjects;
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
        var notesService = serviceProvider.GetRequiredService<INotesService>();

        var nodes = await nodesRepository.ListNodesPaginatedAsync(pageIndex, pageSize, cancellationToken);

        var reminders = await remindersService
            .GetRemindersBaseBelongingToNodeIdsAsync(nodes.Select(n => n.Id).ToList(), cancellationToken);

        var timelines = await timelinesService
            .GetTimelinesBaseBelongingToNodeIdsAsync(nodes.Select(n => n.Id).ToList(), cancellationToken);

        var notes = await notesService
            .GetNotesBaseBelongingToNodeIdsAsync(nodes.Select(n => n.Id).ToList(), cancellationToken);

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
                    .First(t => t.Id == n.TimelineId.ToString()),
                notes
                    .Where(r => n.NoteIds.Select(id => id.ToString()).Contains(r.Id))
                    .Select(r => new NoteBaseDto(
                        id: r.Id!.ToString(),
                        title: r.Title,
                        content: r.Content,
                        timestamp: r.Timestamp,
                        owner: r.Owner,
                        relatedNotes: r.RelatedNotes,
                        sharedWith: r.SharedWith,
                        isPublic: r.IsPublic)
                    )
                    .ToList()
            )
        ).ToList();

        return nodeDtos;
    }

    public async Task<List<NodeBaseDto>> GetNodesByIdsAsync(IEnumerable<NodeId> nodeIds,
        CancellationToken cancellationToken)
    {
        return (await nodesRepository.GetNodesByIdsAsync(nodeIds, cancellationToken)).Select(n => n.ToNodeBaseDto())
            .ToList();
    }

    public async Task<long> CountNodesAsync(CancellationToken cancellationToken)
    {
        return await nodesRepository.NodeCountAsync(cancellationToken);
    }

    public async Task<NodeDto> GetNodeByIdAsync(NodeId nodeId, CancellationToken cancellationToken)
    {
        var timelinesService = serviceProvider.GetRequiredService<ITimelinesService>();
        var remindersService = serviceProvider.GetRequiredService<IRemindersService>();
        var notesService = serviceProvider.GetRequiredService<INotesService>();

        var node = await nodesRepository.GetNodeByIdAsync(nodeId, cancellationToken);

        var timeline = await timelinesService.GetTimelineByIdAsync(node.TimelineId, cancellationToken);

        var reminders = await remindersService
            .GetRemindersBaseBelongingToNodeIdsAsync([node.Id], cancellationToken);

        var notes = await notesService
            .GetNotesBaseBelongingToNodeIdsAsync([node.Id], cancellationToken);

        var nodeDto = node.ToNodeDto(reminders, timeline, notes);

        return nodeDto;
    }

    #endregion

    #region Get

    public async Task<NodeBaseDto> GetNodeBaseByIdAsync(NodeId nodeId, CancellationToken cancellationToken)
    {
        var remindersService = serviceProvider.GetRequiredService<IRemindersService>();
        var timelinesService = serviceProvider.GetRequiredService<ITimelinesService>();
        var notesService = serviceProvider.GetRequiredService<INotesService>();

        var node = await nodesRepository.GetNodeByIdAsync(nodeId, cancellationToken);
        
        var reminders = await remindersService.GetRemindersBaseBelongingToNodeIdsAsync([node.Id], cancellationToken);
        var timeline = await timelinesService.GetTimelineByIdAsync(node.TimelineId, cancellationToken);
        var notes = await notesService.GetNotesBaseBelongingToNodeIdsAsync([node.Id], cancellationToken);

        var nodeDto = node.ToNodeDto(reminders, timeline, notes);

        return nodeDto;
    }

    #endregion

    public async Task AddReminder(NodeId nodeId, ReminderId reminderId, CancellationToken cancellationToken)
    {
        var node = await nodesRepository.GetNodeByIdAsync(nodeId, cancellationToken);
        node.AddReminder(reminderId);

        await nodesRepository.UpdateNodeAsync(node, cancellationToken);
    }

    public Task RemoveReminder(NodeId nodeId, ReminderId reminderId, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task RemoveReminders(NodeId nodeId, IEnumerable<ReminderId> reminderIds, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task DeleteNode(NodeId nodeId, CancellationToken cancellationToken)
    {
        var node = await nodesRepository.GetNodeByIdAsync(nodeId, cancellationToken);

        var timelinesService = serviceProvider.GetRequiredService<ITimelinesService>();
        await timelinesService.RemoveNode(node.TimelineId, node.Id, cancellationToken);
        
        await nodesRepository.DeleteNode(nodeId, cancellationToken);
    }

    public async Task DeleteNodes(TimelineId timelineId, IEnumerable<NodeId> nodeIds, CancellationToken cancellationToken)
    {
        var input = nodeIds.ToList();

        var notesService = serviceProvider.GetRequiredService<INotesService>();
        await notesService.DeleteNotesByNodeIds(input, cancellationToken);

        var timelinesService = serviceProvider.GetRequiredService<ITimelinesService>();
        await timelinesService.RemoveNodes(timelineId, input, cancellationToken);

        await nodesRepository.DeleteNodes(input, cancellationToken);
    }

    #region Relationships

    public async Task RemoveNotes(NodeId nodeId, IEnumerable<NoteId> noteIds, CancellationToken cancellationToken)
    {
        var node = await nodesRepository.GetNodeByIdAsync(nodeId, cancellationToken);

        foreach (var noteId in noteIds)
            node.RemoveNote(noteId);

        await nodesRepository.UpdateNodeAsync(node, cancellationToken);
    }

    public async Task<List<NodeBaseDto>> GetNodesBaseBelongingToTimelineIdsAsync(IEnumerable<TimelineId> timelineIds,
        CancellationToken cancellationToken)
    {
        var nodes = await nodesRepository.GetNodesBelongingToTimelineIdsAsync(timelineIds, cancellationToken);
        var nodeBaseDtos = nodes.Adapt<List<NodeBaseDto>>();
        return nodeBaseDtos;
    }

    #endregion

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
