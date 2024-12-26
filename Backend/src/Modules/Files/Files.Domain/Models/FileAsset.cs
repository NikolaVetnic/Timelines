using Files.Domain.Events;

namespace Files.Domain.Models;

public class FileAsset : Aggregate<FileAssetId>
{
    private readonly List<string> _sharedWith = [];

    public IReadOnlyList<string> SharedWith => _sharedWith.AsReadOnly();

    public required string Name { get; set; }
    public required float Size { get; set; }
    public required string Type { get; set; }
    public required string Owner { get; set; }
    public required string Description { get; set; }

    #region File

    public static FileAsset Create(FileAssetId fileAssetId, string name, float size, string type, string owner, string description, List<string> sharedWith)
    {
        var file = new FileAsset
        {
            Id = fileAssetId,
            Name = name,
            Size = size,
            Type = type,
            Owner = owner,
            Description = description
        };

        foreach (var person in sharedWith)
            file.AddPerson(person);

        file.AddDomainEvent(new FileCreatedEvent(file));

        return file;
    }

    public void Update(string name, float size, string type, string owner, string description)
    {
        Name = name;
        Size = size;
        Type = type;
        Owner = owner;
        Description = description;

        AddDomainEvent(new FileUpdatedEvent(this));
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
