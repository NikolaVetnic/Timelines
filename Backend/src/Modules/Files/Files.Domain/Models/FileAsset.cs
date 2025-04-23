using BuildingBlocks.Domain.Enums;
using BuildingBlocks.Domain.Files.File.Events;
using BuildingBlocks.Domain.Files.File.ValueObjects;
using BuildingBlocks.Domain.Nodes.Node.ValueObjects;

namespace Files.Domain.Models;

public class FileAsset : Aggregate<FileAssetId>
{
    private readonly List<string> _sharedWith = [];
    public IReadOnlyList<string> SharedWith => _sharedWith.AsReadOnly();

    public required string Name { get; set; }
    public required string Description { get; set; }
    public required float Size { get; set; }
    public required EFileType Type { get; set; }
    public required string Owner { get; set; }
    public required byte[] Content { get; set; }
    public required bool IsPublic { get; set; }
    public required NodeId NodeId { get; set; }


    #region File

    public static FileAsset Create(FileAssetId id, string name, string description, float size, EFileType type, string owner, byte[] content, bool isPublic, List<string> sharedWith, NodeId nodeId)
    {
        var file = new FileAsset
        {
            Id = id,
            Name = name,
            Description = description,
            Size = size,
            Type = type,
            Owner = owner,
            Content = content,
            IsPublic = isPublic,
            NodeId = nodeId
        };

        foreach (var person in sharedWith)
            file.AddPerson(person);

        file.AddDomainEvent(new FileAssetCreatedEvent(file.Id));

        return file;
    }

    public void Update(string name, string description, float size, EFileType type, string owner, byte[] content, bool isPublic)
    {
        Name = name;
        Size = size;
        Type = type;
        Owner = owner;
        Description = description;

        AddDomainEvent(new FileAssetUpdatedEvent(Id));
    }

    #endregion
    #region ShadredWith
    private void AddPerson(string person)
    {
        _sharedWith.Add(person);
    }
    private void RemovePerson(string person)
    {
        _sharedWith.Remove(person);
    }
    #endregion
}