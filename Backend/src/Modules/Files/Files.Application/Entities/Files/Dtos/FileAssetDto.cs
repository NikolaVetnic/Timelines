namespace Files.Application.Entities.Files.Dtos;

public record FileAssetDto(
    string Id,
    string Name,
    string Description,
    float Size,
    string Type,
    string Owner,
    string Content,
    bool IsPublic,
    List<string> SharedWith);
