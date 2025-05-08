using BuildingBlocks.Application.Data;
using BuildingBlocks.Domain.Files.File.Dtos;
using BuildingBlocks.Domain.Files.File.ValueObjects;
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
    private IRemindersService RemindersService => serviceProvider.GetRequiredService<IRemindersService>();
    private ITimelinesService TimelinesService => serviceProvider.GetRequiredService<ITimelinesService>();
    private IFilesService FilesService => serviceProvider.GetRequiredService<IFilesService>();
    private INotesService NotesService => serviceProvider.GetRequiredService<INotesService>();

    #region List

    public async Task<List<NodeDto>> ListNodesPaginated(int pageIndex, int pageSize, CancellationToken cancellationToken)
    {
        var nodes = await nodesRepository.ListNodesPaginatedAsync(pageIndex, pageSize, cancellationToken);

        var fileAssets = await FilesService
            .GetFileAssetsBaseBelongingToNodeIdsAsync(nodes.Select(n => n.Id).ToList(), cancellationToken);

        var reminders = await RemindersService
            .GetRemindersBaseBelongingToNodeIdsAsync(nodes.Select(n => n.Id).ToList(), cancellationToken);

        var timelines = await TimelinesService
            .GetTimelinesBaseBelongingToNodeIdsAsync(nodes.Select(n => n.Id).ToList(), cancellationToken);

        var notes = await NotesService
            .GetNotesBaseBelongingToNodeIdsAsync(nodes.Select(n => n.Id).ToList(), cancellationToken);

        var nodeDtos = nodes.Select(n =>
            n.ToNodeDto(timelines
                    .First(t => t.Id == n.TimelineId.ToString()),
                fileAssets
                    .Where(f => n.FileAssetIds.Select(id => id.ToString()).Contains(f.Id))
                    .Select(f => new FileAssetBaseDto(
                        id: f.Id!.ToString(),
                        name: f.Name,
                        description: f.Description,
                        size: f.Size,
                        type: f.Type,
                        owner: f.Owner,
                        content: f.Content,
                        isPublic: f.IsPublic,
                        sharedWith: f.SharedWith)
                    )
                    .ToList(),
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
                        isPublic: r.IsPublic,
                        createdAt: r.CreatedAt,
                        lastModifiedAt: r.LastModifiedAt)
                    )
                    .ToList(),
                reminders
                    .Where(r => n.ReminderIds.Select(id => id.ToString()).Contains(r.Id))
                    .Select(r => new ReminderBaseDto(
                        id: r.Id!.ToString(),
                        title: r.Title,
                        description: r.Description,
                        notifyAt: r.NotifyAt,
                        priority: r.Priority)
                    )
                    .ToList()
            )
        ).ToList();

        return nodeDtos;
    }

    public async Task<List<NodeBaseDto>> ListNodesByTimelineIdPaginated(TimelineId timelineId, int pageIndex, int pageSize, CancellationToken cancellationToken)
    {
        var nodes = await nodesRepository.ListNodesByTimelineIdPaginatedAsync(timelineId, pageIndex, pageSize, cancellationToken);

        var nodesDtos = nodes
            .Select(n => n.ToNodeBaseDto())
            .ToList();

        return nodesDtos;
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

    public async Task<long> CountNodesByTimelineIdAsync(TimelineId timelineId, CancellationToken cancellationToken)
    {
        return await nodesRepository.NodeCountByTimelineIdAsync(timelineId, cancellationToken);
    }

    #endregion

    #region Get

    public async Task<NodeDto> GetNodeByIdAsync(NodeId nodeId, CancellationToken cancellationToken)
    {
        var node = await nodesRepository.GetNodeByIdAsync(nodeId, cancellationToken);

        var timeline = await TimelinesService.GetTimelineByIdAsync(node.TimelineId, cancellationToken);
        var fileAssets = await FilesService.GetFileAssetsBaseBelongingToNodeIdsAsync([node.Id], cancellationToken);
        var notes = await NotesService.GetNotesBaseBelongingToNodeIdsAsync([node.Id], cancellationToken);
        var reminders = await RemindersService.GetRemindersBaseBelongingToNodeIdsAsync([node.Id], cancellationToken);

        var nodeDto = node.ToNodeDto(timeline, fileAssets, notes, reminders);

        return nodeDto;
    }

    #endregion

    #region Get

    public async Task<NodeBaseDto> GetNodeBaseByIdAsync(NodeId nodeId, CancellationToken cancellationToken)
    {
        var node = await nodesRepository.GetNodeByIdAsync(nodeId, cancellationToken);

        var timeline = await TimelinesService.GetTimelineByIdAsync(node.TimelineId, cancellationToken);
        var fileAssets = await FilesService.GetFileAssetsBaseBelongingToNodeIdsAsync([node.Id], cancellationToken);
        var notes = await NotesService.GetNotesBaseBelongingToNodeIdsAsync([node.Id], cancellationToken);
        var reminders = await RemindersService.GetRemindersBaseBelongingToNodeIdsAsync([node.Id], cancellationToken);

        var nodeDto = node.ToNodeDto(timeline, fileAssets, notes, reminders);

        return nodeDto;
    }

    #endregion

    public async Task<NodeId> CloneNodeIntoTimelineAsync(NodeId nodeTemplateId, TimelineId timelineId, CancellationToken cancellationToken)
    {
        var nodeTemplate = await nodesRepository.GetNodeByIdAsync(nodeTemplateId, cancellationToken);

        var node = Node.Create(
            NodeId.Of(Guid.NewGuid()),
            nodeTemplate.Title,
            nodeTemplate.Description,
            nodeTemplate.Phase,
            nodeTemplate.Timestamp,
            nodeTemplate.Importance,
            nodeTemplate.Categories,
            nodeTemplate.Tags,
            timelineId
        );

        await nodesRepository.CreateNodeAsync(node, cancellationToken);

        return node.Id;
    }

    public async Task AddReminder(NodeId nodeId, ReminderId reminderId, CancellationToken cancellationToken)
    {
        var node = await nodesRepository.GetNodeByIdAsync(nodeId, cancellationToken);
        node.AddReminder(reminderId);
        await nodesRepository.UpdateNodeAsync(node, cancellationToken);
    }

    public async Task RemoveReminder(NodeId nodeId, ReminderId reminderId, CancellationToken cancellationToken)
    {
        var node = await nodesRepository.GetNodeByIdAsync(nodeId, cancellationToken);
        node.RemoveReminder(reminderId);

        await nodesRepository.UpdateNodeAsync(node, cancellationToken);
    }

    public async Task RemoveReminders(NodeId nodeId, IEnumerable<ReminderId> reminderIds,
        CancellationToken cancellationToken)
    {
        var node = await nodesRepository.GetTrackedNodeByIdAsync(nodeId, cancellationToken);

        foreach (var reminderId in reminderIds)
            node.RemoveReminder(reminderId);

        await nodesRepository.UpdateNodeAsync(node, cancellationToken);
    }

    public async Task DeleteNode(NodeId nodeId, CancellationToken cancellationToken)
    {
        var node = await nodesRepository.GetNodeByIdAsync(nodeId, cancellationToken);

        var timelinesService = serviceProvider.GetRequiredService<ITimelinesService>();
        await timelinesService.RemoveNode(node.TimelineId, node.Id, cancellationToken);

        await nodesRepository.DeleteNode(nodeId, cancellationToken);
    }

    public async Task DeleteNodes(TimelineId timelineId, IEnumerable<NodeId> nodeIds,
        CancellationToken cancellationToken)
    {
        var input = nodeIds.ToList();

        var timelinesService = serviceProvider.GetRequiredService<ITimelinesService>();
        await timelinesService.RemoveNodes(timelineId, input, cancellationToken);

        await nodesRepository.DeleteNodes(input, cancellationToken);
    }

    #region Relationships

    public async Task RemoveNotes(NodeId nodeId, IEnumerable<NoteId> noteIds, CancellationToken cancellationToken)
    {
        var node = await nodesRepository.GetTrackedNodeByIdAsync(nodeId, cancellationToken);

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

    public async Task AddFileAsset(NodeId nodeId, FileAssetId fileAssetId, CancellationToken cancellationToken)
    {
        var node = await nodesRepository.GetNodeByIdAsync(nodeId, cancellationToken);
        node.AddFileAsset(fileAssetId);

        await nodesRepository.UpdateNodeAsync(node, cancellationToken);
    }

    public async Task RemoveFileAsset(NodeId nodeId, FileAssetId fileAssetId, CancellationToken cancellationToken)
    {
        var node = await nodesRepository.GetNodeByIdAsync(nodeId, cancellationToken);
        node.RemoveFileAsset(fileAssetId);
        await nodesRepository.UpdateNodeAsync(node, cancellationToken);
    }

    public async Task RemoveFileAssets(NodeId nodeId, IEnumerable<FileAssetId> fileAssetIds,
        CancellationToken cancellationToken)
    {
        var node = await nodesRepository.GetTrackedNodeByIdAsync(nodeId, cancellationToken);

        foreach (var fileAssetId in fileAssetIds)
            node.RemoveFileAsset(fileAssetId);

        await nodesRepository.UpdateNodeAsync(node, cancellationToken);
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
