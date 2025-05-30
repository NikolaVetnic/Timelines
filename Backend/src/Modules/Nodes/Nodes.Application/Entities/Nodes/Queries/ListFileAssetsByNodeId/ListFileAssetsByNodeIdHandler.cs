using BuildingBlocks.Application.Data;
using BuildingBlocks.Application.Pagination;
using BuildingBlocks.Domain.Files.File.Dtos;

namespace Nodes.Application.Entities.Nodes.Queries.ListFileAssetsByNodeId;

internal class ListFileAssetsByNodeIdHandler(IFilesService filesService)
    : IQueryHandler<ListFileAssetsByNodeIdQuery, ListFileAssetsByNodeIdResult>
{
    public async Task<ListFileAssetsByNodeIdResult> Handle(ListFileAssetsByNodeIdQuery query, CancellationToken cancellationToken)
    {
        var pageIndex = query.PaginationRequest.PageIndex;
        var pageSize = query.PaginationRequest.PageSize;

        var fileAssets = await filesService.ListFileAssetsByNodeIdPaginated(query.Id, pageIndex, pageSize, cancellationToken);

        return new ListFileAssetsByNodeIdResult(
            new PaginatedResult<FileAssetBaseDto>(
                pageIndex,
                pageSize,
                fileAssets.Count,
                fileAssets));
    }
}
