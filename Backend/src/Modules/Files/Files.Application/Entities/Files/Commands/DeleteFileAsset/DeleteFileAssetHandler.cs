using BuildingBlocks.Application.Data;
using Files.Application.Data.Abstractions;
using Files.Application.Entities.Files.Exceptions;

namespace Files.Application.Entities.Files.Commands.DeleteFileAsset;

public class DeleteFileAssetHandler(IFilesDbContext dbContext, INodesService nodesService) : ICommandHandler<DeleteFileAssetCommand, DeleteFileAssetResult>
{
    public async Task<DeleteFileAssetResult> Handle(DeleteFileAssetCommand command, CancellationToken cancellationToken)
    {
        var fileAsset = await dbContext.FileAssets
            .AsNoTracking()
            .SingleOrDefaultAsync(f => f.Id == command.Id, cancellationToken);

        if (fileAsset is null)
            throw new FileAssetNotFoundException(command.Id.ToString());


        await nodesService.RemoveFileAsset(fileAsset.NodeId, fileAsset.Id, cancellationToken);

        dbContext.FileAssets.Remove(fileAsset);

        await dbContext.SaveChangesAsync(cancellationToken);

        return new DeleteFileAssetResult(true);
    }
}
