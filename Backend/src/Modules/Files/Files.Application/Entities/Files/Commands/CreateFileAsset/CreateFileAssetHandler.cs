using BuildingBlocks.Application.Data;
using BuildingBlocks.Domain.Files.File.ValueObjects;
using Files.Application.Data.Abstractions;

namespace Files.Application.Entities.Files.Commands.CreateFileAsset;

internal class CreateFileAssetHandler(ICurrentUser currentUser, IFilesRepository filesRepository, INodesService nodesService)
    : ICommandHandler<CreateFileAssetCommand, CreateFileAssetResult>
{
    public async Task<CreateFileAssetResult> Handle(CreateFileAssetCommand command, CancellationToken cancellationToken)
    {
        var userId = currentUser.UserId!;
        var fileAsset = command.ToFileAsset(userId);

        await nodesService.EnsureNodeBelongsToOwner(fileAsset.NodeId, cancellationToken);
        await filesRepository.AddFileAssetAsync(fileAsset, cancellationToken);
        await nodesService.AddFileAsset(fileAsset.NodeId, fileAsset.Id, cancellationToken);
        
        return new CreateFileAssetResult(fileAsset.Id);
    }
}

internal static class CreateFileAssetCommandExtensions
{
    public static FileAsset ToFileAsset(this CreateFileAssetCommand command, string userId)
    {
        return FileAsset.Create(
            FileAssetId.Of(Guid.NewGuid()),
            command.Name,
            command.Description,
            command.Size,
            command.Type,
            userId,
            command.Content,
            command.IsPublic,
            command.SharedWith,
            command.NodeId
        );
    }
}
