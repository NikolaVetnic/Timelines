namespace Files.Application.Entities.Files.Dtos;

public record FileAssetDto(
    string Id,
    string Name,
    string Size,
    string Type,
    string Owner,
    string Description,
    List<string> SharedWith);
