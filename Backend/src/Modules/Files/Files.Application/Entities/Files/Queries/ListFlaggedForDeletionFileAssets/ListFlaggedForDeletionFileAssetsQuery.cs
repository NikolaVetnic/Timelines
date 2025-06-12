using BuildingBlocks.Application.Pagination;
using BuildingBlocks.Domain.Files.File.Dtos;

// ReSharper disable ClassNeverInstantiated.Global

namespace Files.Application.Entities.Files.Queries.ListFlaggedForDeletionFileAssets;

public record ListFlaggedForDeletionFileAssetsQuery(PaginationRequest PaginationRequest) : IQuery<ListFlaggedForDeletionFileAssetsResult>;

public record ListFlaggedForDeletionFileAssetsResult(PaginatedResult<FileAssetDto> FileAssets);
