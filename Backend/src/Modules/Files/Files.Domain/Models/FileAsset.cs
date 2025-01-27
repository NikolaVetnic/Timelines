using Files.Domain.Events;

namespace Files.Domain.Models;

public class FileAsset : Aggregate<FileAssetId>
{
    private readonly List<string> _sharedWith = [];
    //private readonly List<FileAsset> _related = [];

    public IReadOnlyList<string> SharedWith => _sharedWith.AsReadOnly();

    public required string Name { get; set; }
    public required string Description { get; set; }
    public required float Size { get; set; }
    public required EFileType Type { get; set; }
    public required string Owner { get; set; }
    public required byte[] Content { get; set; }
    public required bool IsPublic { get; set; }

    #region File

    public static FileAsset Create(FileAssetId id, string name, string description, float size, EFileType type, string owner, byte[] content, bool isPublic, List<string> sharedWith)
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
            IsPublic = isPublic
        };

        foreach (var person in sharedWith)
            file.AddPerson(person);

        file.AddDomainEvent(new FileAssetCreatedEvent(file));

        return file;
    }

    public void Update(string name, string description, float size, EFileType type, string owner, byte[] content, bool isPublic)
    {
        Name = name;
        Description = description;
        Size = size;
        Type = type;
        Owner = owner;
        Content = content;
        IsPublic = isPublic;

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

public enum EFileType
{
    Pdf,     // Represents PDF files
    Docx,    // Represents Word documents
    Txt,     // Represents plain text files
    Csv,     // Represents CSV files
    Image,   // Represents image files (e.g., jpg, png)
    Video    // Represents video files
}
