namespace BuildingBlocks.Domain.Files.File.Dtos;

public class FileAssetDto(
    string id,
    string name,
    float size,
    string type,
    string owner,
    string description,
    List<string> sharedWith);
