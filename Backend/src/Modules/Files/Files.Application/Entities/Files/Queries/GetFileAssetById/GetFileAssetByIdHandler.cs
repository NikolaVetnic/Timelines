using Files.Application.Data.Abstractions;
using Files.Application.Entities.Files.Exceptions;
using Files.Application.Entities.Files.Extensions;

namespace Files.Application.Entities.Files.Queries.GetFileAssetById;

internal class GetFileAssetByIdHandler(IFilesDbContext dbContext) : IQueryHandler<GetFileAssetByIdQuery, GetFileAssetByIdResult>
{
    public async Task<GetFileAssetByIdResult> Handle(GetFileAssetByIdQuery query, CancellationToken cancellationToken)
    {
        var fileAsset = await dbContext.FileAssets
            .AsNoTracking()
            .SingleOrDefaultAsync(f => f.Id == query.Id, cancellationToken);

        if (fileAsset is null)
            throw new FileAssetNotFoundException(query.Id.ToString());

        return new GetFileAssetByIdResult(fileAsset.ToFileAssetDto());
    }
}
