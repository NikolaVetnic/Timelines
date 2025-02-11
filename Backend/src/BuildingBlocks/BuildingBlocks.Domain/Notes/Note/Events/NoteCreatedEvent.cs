using BuildingBlocks.Domain.Abstractions;
using BuildingBlocks.Domain.ValueObjects.Ids;

namespace BuildingBlocks.Domain.Notes.Note.Events
{
    public record NoteCreatedEvent(NoteId NoteId) : IDomainEvent;
}
