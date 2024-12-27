using Notes.Domain.Models;

namespace Notes.Domain.Events;

public record NoteUpdatedEvent(Note Note) : IDomainEvent;
