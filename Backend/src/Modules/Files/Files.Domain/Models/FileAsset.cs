using Files.Domain.Events;

namespace Files.Domain.Models;

public class FileAsset : Aggregate<FileAssetId>
{
    public required string Title { get; set; }

    #region File

    public static FileAsset Create(FileAssetId assetId, string title)
    {
        var file = new FileAsset
        {
            Id = assetId,
            Title = title
        };

        file.AddDomainEvent(new FileCreatedEvent(file));

        return file;
    }

    public void Update(string title)
    {
        Title = title;

        AddDomainEvent(new FileUpdatedEvent(this));
    }

    #endregion
}
