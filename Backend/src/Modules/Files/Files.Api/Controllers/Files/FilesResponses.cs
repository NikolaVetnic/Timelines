using BuildingBlocks.Application.Pagination;
using BuildingBlocks.Domain.Files.File.Dtos;
using BuildingBlocks.Domain.Files.File.ValueObjects;
using System.Text.Json.Serialization;

namespace Files.Api.Controllers.Files;

public record CreateFileAssetResponse(FileAssetId Id);

public record GetFileAssetByIdResponse([property: JsonPropertyName("file")] FileAssetDto FileAssetDto);

public record ListFileAssetsResponse(PaginatedResult<FileAssetDto> FileAssets);
