using BuildingBlocks.Application.Data;
using BuildingBlocks.Domain.Nodes.Node.ValueObjects;
using Files.Application.Data.Abstractions;
using Files.Application.Entities.Files.Exceptions;
using Files.Application.Entities.Files.Extensions;

namespace Files.Application.Entities.Files.Commands.UpdateFileAsset;

internal class UpdateFileAssetHandler(IFilesRepository filesRepository, INodesService nodesService)
    : ICommandHandler<UpdateFileAssetCommand, UpdateFileAssetResult>
{
    public async Task<UpdateFileAssetResult> Handle(UpdateFileAssetCommand command, CancellationToken cancellationToken)
    {
        var fileAsset =
            await filesRepository.GetFileAssetByIdAsync(command.Id, cancellationToken);

        if (fileAsset == null)
            throw new FileAssetNotFoundException(command.Id.ToString());

        fileAsset.Name = command.Name ?? fileAsset.Name;
        fileAsset.Description = command.Description ?? fileAsset.Description;
        fileAsset.SharedWith = command.SharedWith ?? fileAsset.SharedWith;
        fileAsset.IsPublic = command.IsPublic ?? fileAsset.IsPublic;

        var node = await nodesService.GetNodeByIdAsync(
            command.NodeId ?? fileAsset.NodeId, cancellationToken);

        if (node?.Id == null)
            throw new NotFoundException($"Related with id {command.NodeId} not found");

        fileAsset.NodeId = NodeId.Of(Guid.Parse(node.Id));

        await filesRepository.UpdateFileAssetAsync(fileAsset, cancellationToken);

        return new UpdateFileAssetResult(fileAsset.ToFileAssetDto(node));
    }
}
