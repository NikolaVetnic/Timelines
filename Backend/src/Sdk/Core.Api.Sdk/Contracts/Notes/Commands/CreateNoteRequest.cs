using Core.Api.Sdk.Contracts.Notes.Dtos;

namespace Core.Api.Sdk.Contracts.Notes.Commands;

public class CreateNoteRequest
{
    public required NoteDto Note { get; init; }
}
