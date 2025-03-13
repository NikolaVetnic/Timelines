using BuildingBlocks.Application.Data;
using BuildingBlocks.Domain.Files.File.ValueObjects;
using BuildingBlocks.Domain.Nodes.Node.Dtos;
using BuildingBlocks.Domain.Nodes.Node.ValueObjects;
using BuildingBlocks.Domain.Reminders.Reminder.ValueObjects;
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
        var nodeDto = node.ToNodeDto();

        var remindersService = serviceProvider.GetRequiredService<IRemindersService>();
        var fileAssetsService = serviceProvider.GetRequiredService<IFileAssetsService>();

        foreach (var reminderId in node.ReminderIds)
        {
            var reminder = await remindersService.GetReminderBaseByIdAsync(reminderId, cancellationToken);
            nodeDto.Reminders.Add(reminder);
        }

        foreach (var fileAssetId in node.FileAssetIds)
        {
            var fileAsset = await fileAssetsService.GetFileAssetByIdAsync(fileAssetId, cancellationToken);
            nodeDto.FileAssets.Add(fileAsset);
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

    public async Task AddFileAsset(NodeId nodeId, FileAssetId fileAssetId, CancellationToken cancellationToken)
    {
        var node = await nodesRepository.GetNodeByIdAsync(nodeId, cancellationToken);
        node.AddFileAsset(fileAssetId);
        await nodesRepository.UpdateNodeAsync(node, cancellationToken);
    }
}
