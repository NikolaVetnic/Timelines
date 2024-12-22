using Notes.Domain.Models;

namespace Notes.Domain.Events
{
    public record NoteCreatedEvent(Note Note) : IDomainEvent;
}
