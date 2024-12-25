using BuildingBlocks.Domain.Abstractions;
using BuildingBlocks.Domain.ValueObjects.Ids;
using Files.Domain.Events;

namespace Files.Domain.Models;

public class File : Aggregate<FileId>
{
    public required string Title { get; set; }

    #region File

    public static File Create(FileId id, string title)
    {
        var file = new File
        {
            Id = id,
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
