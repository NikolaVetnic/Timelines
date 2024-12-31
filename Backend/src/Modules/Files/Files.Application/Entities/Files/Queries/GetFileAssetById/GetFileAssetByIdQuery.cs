// ReSharper disable ClassNeverInstantiated.Global

using Files.Application.Entities.Files.Dtos;

namespace Files.Application.Entities.Files.Queries.GetFileAssetById;

public record GetFileAssetByIdQuery(string Id) : IQuery<GetFileAssetByIdResult>;

// ReSharper disable once NotAccessedPositionalProperty.Global
public record GetFileAssetByIdResult(FileAssetDto FileAssetDto);
