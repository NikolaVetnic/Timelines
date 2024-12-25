using BuildingBlocks.Domain.ValueObjects.Ids;
using Files.Application.Data;

namespace Files.Application.Files.Commands.CreateFile;

public class CreateFileHandler(IFilesDbContext dbContext) :
    ICommandHandler<CreateFileCommand, CreateFileResult>
{
    public async Task<CreateFileResult> Handle(CreateFileCommand command, CancellationToken cancellationToken)
    {
        var fileAsset = FileAsset.Create(
            FileAssetId.Of(Guid.NewGuid()),
            command.FileAsset.Title
        );

        dbContext.FileAssets.Add(fileAsset);
        await dbContext.SaveChangesAsync(cancellationToken);

        return new CreateFileResult(fileAsset.Id);
    }
}
