using Core.Api.Sdk.Contracts.Notes.ValueObjects;

namespace Core.Api.Sdk.Contracts.Notes.Commands;

public class CreateNoteResponse
{
    public required NoteId Id { get; init; }
}
