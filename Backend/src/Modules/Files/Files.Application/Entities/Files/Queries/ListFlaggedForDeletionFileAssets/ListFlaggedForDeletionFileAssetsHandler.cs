using BuildingBlocks.Application.Data;
using BuildingBlocks.Application.Pagination;
using BuildingBlocks.Domain.Files.File.Dtos;

namespace Files.Application.Entities.Files.Queries.ListFlaggedForDeletionFileAssets;

internal class ListFlaggedForDeletionFileAssetsHandler(IFilesService filesService) : IQueryHandler<ListFlaggedForDeletionFileAssetsQuery, ListFlaggedForDeletionFileAssetsResult>
{
    public async Task<ListFlaggedForDeletionFileAssetsResult> Handle(ListFlaggedForDeletionFileAssetsQuery query, CancellationToken cancellationToken)
    {
        var pageIndex = query.PaginationRequest.PageIndex;
        var pageSize = query.PaginationRequest.PageSize;

        var fileAssets = await filesService.ListFlaggedForDeletionFileAssetsPaginated(pageIndex, pageSize, cancellationToken);

        return new ListFlaggedForDeletionFileAssetsResult(
            new PaginatedResult<FileAssetDto>(
                pageIndex,
                pageSize,
                fileAssets.Count,
                fileAssets));
    }
}
