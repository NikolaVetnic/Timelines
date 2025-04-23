using BuildingBlocks.Application.Data;
using BuildingBlocks.Application.Pagination;
using BuildingBlocks.Domain.Files.File.Dtos;
using Files.Application.Data.Abstractions;
using Files.Application.Entities.Files.Extensions;

namespace Files.Application.Entities.Files.Queries.ListFileAssets;

public class ListFileAssetsHandler(IFilesDbContext dbContext, INodesService nodesService) : IQueryHandler<ListFileAssetsQuery, ListFileAssetsResult>
{
    public async Task<ListFileAssetsResult> Handle(ListFileAssetsQuery query, CancellationToken cancellationToken)
    {
        var pageIndex = query.PaginationRequest.PageIndex;
        var pageSize = query.PaginationRequest.PageSize;

        var totalCount = await dbContext.FileAssets.LongCountAsync(cancellationToken);

        var fileAssets = await dbContext.FileAssets
            .AsNoTracking()
            .OrderBy(n => n.CreatedBy)
            .Skip(pageSize * pageIndex)
            .Take(pageSize)
            .ToListAsync(cancellationToken: cancellationToken);

        var fileAssetsDtos = fileAssets.Select(f =>
        {
            var node = nodesService.GetNodeBaseByIdAsync(f.NodeId, cancellationToken).GetAwaiter().GetResult();
            return f.ToFileAssetDto(node);
        }).ToList();

        return new ListFileAssetsResult(
            new PaginatedResult<FileAssetDto>(
                pageIndex,
                pageSize,
                totalCount,
                fileAssetsDtos));
    }
}
