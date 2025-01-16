using BuildingBlocks.Application.Pagination;

// ReSharper disable ClassNeverInstantiated.Global

namespace Files.Application.Entities.Files.Queries.ListFileAssets;

public record ListFileAssetsQuery(PaginationRequest PaginationRequest) : IQuery<ListFileAssetsResult>;

public record ListFileAssetsResult(PaginatedResult<FileAssetDto> FileAssets);
