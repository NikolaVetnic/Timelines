using Files.Application.Entities.Files.Exceptions;
using Files.Application.Entities.Files.Extensions;

namespace Files.Application.Entities.Files.Queries.GetFileAssetById;

internal class GetFileAssetByIdHandler(IFilesDbContext dbContext) : IQueryHandler<GetFileAssetByIdQuery, GetFileAssetByIdResult>
{
    public async Task<GetFileAssetByIdResult> Handle(GetFileAssetByIdQuery query, CancellationToken cancellationToken)
    {
        var fileAssetId = query.Id.ToString();

        var fileAsset = await dbContext.FileAssets
            .AsNoTracking()
            .SingleOrDefaultAsync(f => f.Id == FileAssetId.Of(Guid.Parse(fileAssetId)), cancellationToken);

        if (fileAsset is null)
            throw new FileAssetNotFoundException(fileAssetId);

        return new GetFileAssetByIdResult(fileAsset.ToFileAssetDto());
    }
}
