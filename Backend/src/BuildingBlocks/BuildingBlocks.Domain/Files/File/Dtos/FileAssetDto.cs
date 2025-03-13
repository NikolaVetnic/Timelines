using BuildingBlocks.Domain.Enums;

namespace BuildingBlocks.Domain.Files.File.Dtos;

public class FileAssetDto(
    string id,
    string name,
    string description,
    float size,
    EFileType type,
    string owner,
    byte[] content,
    bool isPublic,
    List<string> sharedWith)
    : FileAssetBaseDto(id, name, description, size, type, owner, content, isPublic, sharedWith)
{

}
