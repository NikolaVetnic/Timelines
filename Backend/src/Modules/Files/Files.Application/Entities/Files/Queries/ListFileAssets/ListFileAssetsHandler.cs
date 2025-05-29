using BuildingBlocks.Application.Data;
using BuildingBlocks.Application.Pagination;
using BuildingBlocks.Domain.Files.File.Dtos;

namespace Files.Application.Entities.Files.Queries.ListFileAssets;

internal class ListFileAssetsHandler(IFilesService filesService)
    : IQueryHandler<ListFileAssetsQuery, ListFileAssetsResult>
{
    public async Task<ListFileAssetsResult> Handle(ListFileAssetsQuery query, CancellationToken cancellationToken)
    {
        var pageIndex = query.PaginationRequest.PageIndex;
        var pageSize = query.PaginationRequest.PageSize;

        var totalCount = await filesService.CountFileAssetsAsync(cancellationToken);

        var fileAssets = await filesService.ListFileAssetsPaginated(pageIndex, pageSize, cancellationToken);

        return new ListFileAssetsResult(
            new PaginatedResult<FileAssetDto>(
                pageIndex,
                pageSize,
                totalCount,
                fileAssets));
    }
}
