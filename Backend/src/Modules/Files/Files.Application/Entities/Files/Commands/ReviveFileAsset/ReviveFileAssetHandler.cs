using BuildingBlocks.Application.Data;
using Files.Application.Entities.Files.Exceptions;

namespace Files.Application.Entities.Files.Commands.ReviveFileAsset;

internal class ReviveFileAssetHandler(IFilesService filesService) : ICommandHandler<ReviveFileAssetCommand, ReviveFileAssetResponse>
{
    public async Task<ReviveFileAssetResponse> Handle(ReviveFileAssetCommand command, CancellationToken cancellationToken)
    {
        var fileAsset = await filesService.GetFileAssetBaseByIdAsync(command.Id, cancellationToken);

        if (fileAsset is null)
            throw new FileAssetNotFoundException(command.Id.ToString());

        await filesService.ReviveFileAsset(command.Id, cancellationToken);

        return new ReviveFileAssetResponse(true);
    }
}
