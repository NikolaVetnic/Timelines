using Files.Application.Entities.Files.Exceptions;

namespace Files.Application.Entities.Files.Commands.UpdateFileAsset;

public class UpdateFileAssetHandler(IFilesDbContext dbContext) : ICommandHandler<UpdateFileAssetCommand, UpdateFileAssetResult>
{
    public async Task<UpdateFileAssetResult> Handle(UpdateFileAssetCommand command, CancellationToken cancellationToken)
    {
        var fileAsset = await dbContext.FileAssets
            .AsNoTracking()
            .SingleOrDefaultAsync(f => f.Id == FileAssetId.Of(Guid.Parse(command.FileAsset.Id)), cancellationToken);

        if (fileAsset is null)
            throw new FileAssetNotFoundException(command.FileAsset.Id);

        UpdateFileAssetWithNewValues(fileAsset, command.FileAsset);

        dbContext.FileAssets.Update(fileAsset);
        await dbContext.SaveChangesAsync(cancellationToken);

        return new UpdateFileAssetResult(true);
    }

    private static void UpdateFileAssetWithNewValues(FileAsset fileAsset, FileAssetDto fileAssetDto)
    {
        fileAsset.Update(
            fileAssetDto.Name,
            fileAssetDto.Size,
            fileAssetDto.Type,
            fileAssetDto.Owner,
            fileAssetDto.Description);
    }
}
