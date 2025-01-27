namespace Files.Application.Entities.Files.Commands.CreateFileAsset;

internal class CreateFileAssetHandler(IFilesDbContext dbContext) :
    ICommandHandler<CreateFileAssetCommand, CreateFileAssetResult>
{
    public async Task<CreateFileAssetResult> Handle(CreateFileAssetCommand command, CancellationToken cancellationToken)
    {
        var fileAsset = command.ToFileAsset();

        dbContext.FileAssets.Add(fileAsset);
        await dbContext.SaveChangesAsync(cancellationToken);

        return new CreateFileAssetResult(fileAsset.Id);
    }
}

internal static class CreateFileAssetCommandExtensions
{
    public static FileAsset ToFileAsset(this CreateFileAssetCommand command)
    {
        return FileAsset.Create(
            FileAssetId.Of(Guid.NewGuid()),
            command.FileAsset.Name,
            command.FileAsset.Description,
            command.FileAsset.Size,
            command.FileAsset.Type,
            command.FileAsset.Owner,
            command.FileAsset.Content,
            command.FileAsset.IsPublic,
            command.FileAsset.SharedWith
        );
    }
}
