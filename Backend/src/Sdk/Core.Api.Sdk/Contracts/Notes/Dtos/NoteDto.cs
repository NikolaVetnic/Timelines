namespace Core.Api.Sdk.Contracts.Notes.Dtos;

public class NoteDto
{
    public string? Id { get; set; }
    public required string Title { get; init; }
    public required string Content { get; init; }
    public required DateTime Timestamp { get; init; }
    public required int Importance { get; init; }
}
