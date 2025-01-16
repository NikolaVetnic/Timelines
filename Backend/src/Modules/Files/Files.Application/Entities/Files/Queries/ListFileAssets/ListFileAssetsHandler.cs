using BuildingBlocks.Application.Pagination;
using Files.Application.Entities.Files.Extensions;

namespace Files.Application.Entities.Files.Queries.ListFileAssets;

public class ListFileAssetsHandler(IFilesDbContext dbContext) : IQueryHandler<ListFileAssetsQuery, ListFileAssetsResult>
{
    public async Task<ListFileAssetsResult> Handle(ListFileAssetsQuery query, CancellationToken cancellationToken)
    {
        var pageIndex = query.PaginationRequest.PageIndex;
        var pageSize = query.PaginationRequest.PageSize;

        var totalCount = await dbContext.FileAssets.LongCountAsync(cancellationToken);

        var nodes = await dbContext.FileAssets
            .AsNoTracking()
            .OrderBy(n => n.CreatedAt)
            .Skip(pageSize * pageIndex)
            .Take(pageSize)
            .ToListAsync(cancellationToken: cancellationToken);

        return new ListFileAssetsResult(
            new PaginatedResult<FileAssetDto>(
                pageIndex,
                pageSize,
                totalCount,
                nodes.ToFileAssetDtoList()));
    }
}
