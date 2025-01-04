using Files.Application.Entities.Files.Exceptions;

namespace Files.Application.Entities.Files.Commands.DeleteFileAsset;

public class DeleteFileAssetHandler(IFilesDbContext dbContext) : ICommandHandler<DeleteFileAssetCommand, DeleteFileAssetResult>
{
    public async Task<DeleteFileAssetResult> Handle(DeleteFileAssetCommand command, CancellationToken cancellationToken)
    {
        var fileAsset = await dbContext.FileAssets
            .AsNoTracking()
            .SingleOrDefaultAsync(f => f.Id == FileAssetId.Of(Guid.Parse(command.FileAssetId)), cancellationToken);

        if (fileAsset is null)
            throw new FileAssetNotFoundException(command.FileAssetId);


        dbContext.FileAssets.Remove(fileAsset);
        await dbContext.SaveChangesAsync(cancellationToken);

        return new DeleteFileAssetResult(true);
    }
}
