using BuildingBlocks.Application.Pagination;
using BuildingBlocks.Domain.Files.File.Dtos;

// ReSharper disable ClassNeverInstantiated.Global

namespace Files.Application.Entities.Files.Queries.ListFileAssets;

public record ListFileAssetsQuery(PaginationRequest PaginationRequest) : IQuery<ListFileAssetsResult>;

public record ListFileAssetsResult(PaginatedResult<FileAssetDto> FileAssets);
