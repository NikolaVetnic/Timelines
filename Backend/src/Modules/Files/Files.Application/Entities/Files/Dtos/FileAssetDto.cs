namespace Files.Application.Entities.Files.Dtos;

public record FileAssetDto(
    string Id,
    string Name,
    string Description,
    float Size,
    EFileType Type,
    string Owner,
    byte[] Content,
    bool IsPublic,
    List<string> SharedWith);
