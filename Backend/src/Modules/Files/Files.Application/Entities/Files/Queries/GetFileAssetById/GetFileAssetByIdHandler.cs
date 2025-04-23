using BuildingBlocks.Application.Data;
using Files.Application.Entities.Files.Exceptions;

namespace Files.Application.Entities.Files.Queries.GetFileAssetById;

internal class GetFileAssetByIdHandler(IFilesService fileAssetsService) : IQueryHandler<GetFileAssetByIdQuery, GetFileAssetByIdResult>
{
    public async Task<GetFileAssetByIdResult> Handle(GetFileAssetByIdQuery query, CancellationToken cancellationToken)
    {
        var fileAsset = await fileAssetsService.GetFileAssetByIdAsync(query.Id, cancellationToken);

        if (fileAsset is null)
            throw new FileAssetNotFoundException(query.Id.ToString());

        return new GetFileAssetByIdResult(fileAsset);
    }
}