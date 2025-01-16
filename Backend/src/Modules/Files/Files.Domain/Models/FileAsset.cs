using Files.Domain.Events;

namespace Files.Domain.Models;

public class FileAsset : Aggregate<FileAssetId>
{
    private readonly List<string> _sharedWith = [];

    public IReadOnlyList<string> SharedWith => _sharedWith.AsReadOnly();

    public required string Name { get; set; }
    public required string Size { get; set; }
    public required string Type { get; set; }
    public required string Owner { get; set; }
    public required string Description { get; set; }

    #region File

    public static FileAsset Create(FileAssetId id, string name, string size, string type, string owner, string description, List<string> sharedWith)
    {
        var file = new FileAsset
        {
            Id = id,
            Name = name,
            Size = size,
            Type = type,
            Owner = owner,
            Description = description
        };

        foreach (var person in sharedWith)
            file.AddPerson(person);

        file.AddDomainEvent(new FileAssetCreatedEvent(file));

        return file;
    }

    public void Update(string? name, string? size, string? type, string? owner, string? description)
    {
        if (name != null)
            Name = name;

        if (size != null)
            Size = size;

        if (type != null)
            Type = type;

        if (owner != null) 
            Owner = owner;

        if (description != null)
            Description = description;

        AddDomainEvent(new FileAssetUpdatedEvent(this));
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
