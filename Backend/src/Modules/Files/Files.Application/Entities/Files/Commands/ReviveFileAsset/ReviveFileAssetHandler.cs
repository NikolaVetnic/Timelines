using BuildingBlocks.Application.Data;
using Files.Application.Entities.Files.Exceptions;

namespace Files.Application.Entities.Files.Commands.ReviveFileAsset;

internal class ReviveFileAssetHandler(IFilesService filesService) : ICommandHandler<ReviveFileAssetCommand, ReviveFileAssetResult>
{
    public async Task<ReviveFileAssetResult> Handle(ReviveFileAssetCommand command, CancellationToken cancellationToken)
    {
        var fileAsset = await filesService.GetFileAssetBaseByIdAsync(command.Id, cancellationToken);

        await filesService.ReviveFileAsset(command.Id, cancellationToken);

        return new ReviveFileAssetResult(true);
    }
}
