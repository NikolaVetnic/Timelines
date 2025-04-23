using BuildingBlocks.Application.Data;
using BuildingBlocks.Domain.Files.File.ValueObjects;
using Files.Application.Data.Abstractions;

namespace Files.Application.Entities.Files.Commands.CreateFileAsset;

internal class CreateFileAssetHandler(IFilesDbContext dbContext, INodesService nodesService) : ICommandHandler<CreateFileAssetCommand, CreateFileAssetResult>
{
    public async Task<CreateFileAssetResult> Handle(CreateFileAssetCommand command, CancellationToken cancellationToken)
    {
        var fileAsset = command.ToFileAsset();
        dbContext.FileAssets.Add(fileAsset);
        await dbContext.SaveChangesAsync(cancellationToken);

        await nodesService.AddFileAsset(fileAsset.NodeId, fileAsset.Id, cancellationToken);

        return new CreateFileAssetResult(fileAsset.Id);
    }
}
internal static class CreateFileAssetCommandExtensions
{
    public static FileAsset ToFileAsset(this CreateFileAssetCommand command)
    {
        return FileAsset.Create(
            FileAssetId.Of(Guid.NewGuid()),
            command.Name,
            command.Description,
            command.Size,
            command.Type,
            command.Owner,
            command.Content,
            command.IsPublic,
            command.SharedWith,
            command.NodeId
        );
    }
}