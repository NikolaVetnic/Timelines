using BuildingBlocks.Domain.Enums;
using BuildingBlocks.Domain.Files.File.Events;
using BuildingBlocks.Domain.Files.File.ValueObjects;
using BuildingBlocks.Domain.Nodes.Node.ValueObjects;

namespace Files.Domain.Models;

public class FileAsset : Aggregate<FileAssetId>
{
    public required string Name { get; set; }
    public required string Description { get; set; }
    public required float Size { get; set; }
    public required EFileType Type { get; set; }
    public required string OwnerId { get; set; }
    public required byte[] Content { get; set; }
    public List<string> SharedWith { get; set; } = [];
    public required bool IsPublic { get; set; }
    public required NodeId NodeId { get; set; }

    #region File

    public static FileAsset Create(FileAssetId id, string name, string description, float size, EFileType type, string ownerId, byte[] content, bool isPublic, List<string>? sharedWith, NodeId nodeId)
    {
        var file = new FileAsset
        {
            Id = id,
            Name = name,
            Description = description,
            Size = size,
            Type = type,
            OwnerId = ownerId,
            Content = content,
            IsPublic = isPublic,
            NodeId = nodeId
        };

        foreach (var person in sharedWith)
            file.AddPerson(person);

        file.AddDomainEvent(new FileAssetCreatedEvent(file.Id));

        return file;
    }

    public void Update(string name, string description, float size, EFileType type, string ownerId, byte[] content, bool isPublic)
    {
        Name = name;
        Size = size;
        Type = type;
        OwnerId = ownerId;
        Description = description;

        AddDomainEvent(new FileAssetUpdatedEvent(Id));
    }

    #endregion

    #region ShadredWith
    private void AddPerson(string person)
    {
        SharedWith.Add(person);
    }
    private void RemovePerson(string person)
    {
        SharedWith.Remove(person);
    }

    #endregion
}
