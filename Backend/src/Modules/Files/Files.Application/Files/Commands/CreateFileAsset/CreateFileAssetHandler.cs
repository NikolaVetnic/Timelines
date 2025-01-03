using BuildingBlocks.Domain.ValueObjects.Ids;
using Files.Application.Data;
namespace Files.Application.Files.Commands.CreateFileAsset;

internal class CreateFileAssetHandler(IFilesDbContext dbContext) :
    ICommandHandler<CreateFileAssetCommand, CreateFileAssetResult>
{
    public async Task<CreateFileAssetResult> Handle(CreateFileAssetCommand command, CancellationToken cancellationToken)
    {
        var fileAsset = FileAsset.Create(
            FileAssetId.Of(Guid.NewGuid()),
            command.FileAsset.Name,
            command.FileAsset.Size,
            command.FileAsset.Type,
            command.FileAsset.Owner,
            command.FileAsset.Description,
            command.FileAsset.SharedWith
        );

        dbContext.FileAssets.Add(fileAsset);
        await dbContext.SaveChangesAsync(cancellationToken);

        return new CreateFileAssetResult(fileAsset.Id);
    }
}
