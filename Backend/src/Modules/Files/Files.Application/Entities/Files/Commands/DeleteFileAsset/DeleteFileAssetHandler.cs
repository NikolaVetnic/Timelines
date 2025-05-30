using BuildingBlocks.Application.Data;
using Files.Application.Entities.Files.Exceptions;

namespace Files.Application.Entities.Files.Commands.DeleteFileAsset;

internal class DeleteFileAssetHandler(IFilesService filesService)
    : ICommandHandler<DeleteFileAssetCommand, DeleteFileAssetResult>
{
    public async Task<DeleteFileAssetResult> Handle(DeleteFileAssetCommand command, CancellationToken cancellationToken)
    {
        var fileAsset = await filesService.GetFileAssetBaseByIdAsync(command.Id, cancellationToken);

        if (fileAsset is null)
            throw new FileAssetNotFoundException(command.Id.ToString());

        await filesService.DeleteFileAsset(command.Id, cancellationToken);

        return new DeleteFileAssetResult(true);
    }
}
